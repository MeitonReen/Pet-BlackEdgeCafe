using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.ETagCache.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cafe.Infrastructure.OpenAPI.OperationFilters
{
	public class OpenAPIETagCache : IOperationFilter
	{
		private readonly AppSettings _appSettings = null;

		public OpenAPIETagCache(AppSettings appSettings)
		{
			_appSettings = appSettings;
		}
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			bool IsExistsETagCacheAttribute = context.ApiDescription.CustomAttributes()
				.Any(Attribute => Attribute.GetType() == typeof(ETagCacheAttribute));

			if (IsExistsETagCacheAttribute)
			{
				DefaultHttpContext defaultHttpContext = new();
				ETagCacheService.AddETagHeaders("ETagValueExample", defaultHttpContext);

				operation.Responses.Add(StatusCodes.Status304NotModified.ToString(),
					new OpenApiResponse()
					{
						Description = "There were no changes",
						Headers = HttpHeadersToOpenAPIHeaders(defaultHttpContext
							.Response.Headers),
						Content = new Dictionary<string, OpenApiMediaType>()
							{{ MimeTypes.Application.Json, new OpenApiMediaType()
							{
								Schema = new OpenApiSchema()
								{
									Reference = new OpenApiReference()
									{
										Id = nameof(EmptyResult),
										Type = ReferenceType.Schema
									}
								}
							}}}
					}
				);
			}
			return;
		}
		private static IDictionary<string, OpenApiHeader> HttpHeadersToOpenAPIHeaders(
		IHeaderDictionary httpHeaders)
		{
			var httpHeadersAsArray = (httpHeaders as IEnumerable<KeyValuePair<
				String, StringValues>>).ToArray();

			Dictionary<string, OpenApiHeader> OpenAPIETagHeaders = new();
			Array.ForEach(httpHeadersAsArray, header =>
				OpenAPIETagHeaders.Add(header.Key, new OpenApiHeader()
				{
					Required = true,
					Schema = new OpenApiSchema()
					{
						Type = typeof(string).Name.ToLower(),
						Example = new OpenApiString(header.Value.Aggregate(
							(valueLeft, valueRight) => valueLeft + ", " + valueRight))
					}

				})
			);
			return OpenAPIETagHeaders;
		}
	}
}
