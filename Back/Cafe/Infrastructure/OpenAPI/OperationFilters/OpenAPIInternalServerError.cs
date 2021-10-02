using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Cafe.Infrastructure.OpenAPI.OperationFilters
{
	public class OpenAPIInternalServerError : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			operation.Responses.Add(StatusCodes.Status500InternalServerError.ToString(),
				new OpenApiResponse()
				{
					Description = "Internal server error",
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
				});
			return;
		}
	}
}
