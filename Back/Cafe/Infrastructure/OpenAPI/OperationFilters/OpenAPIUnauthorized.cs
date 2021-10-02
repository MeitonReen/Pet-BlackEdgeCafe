using Cafe.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Cafe.Infrastructure.OpenAPI.OperationFilters
{
	public class OpenAPIUnauthorized : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			bool IsExistsAuthorizeAttribute = context.ApiDescription.CustomAttributes()
				.Any(Attribute => Attribute.GetType() == typeof(AuthorizeAttribute));

			bool IsExistsAllowAnonymousAttribute = context.ApiDescription.CustomAttributes()
				.Any(Attribute => Attribute.GetType() == typeof(AllowAnonymousAttribute));

			if (IsExistsAuthorizeAttribute && !IsExistsAllowAnonymousAttribute)
			{
				operation.Responses.Add(StatusCodes.Status401Unauthorized.ToString(),
					new OpenApiResponse()
					{
						Description = "Access denied/Bad cookie",
						Content = new Dictionary<string, OpenApiMediaType>()
							{{ MimeTypes.Application.Json, new OpenApiMediaType()
							{
									Schema = new OpenApiSchema()
									{
										Reference = new OpenApiReference()
										{
											Id = nameof(ErrorDTO),
											Type = ReferenceType.Schema
										}
									}
							}}}
					});
			}
			return;
		}
	}
}
