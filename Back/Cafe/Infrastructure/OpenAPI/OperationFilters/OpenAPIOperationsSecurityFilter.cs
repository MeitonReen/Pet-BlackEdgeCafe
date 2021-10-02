using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Cafe.Infrastructure.OpenAPI.OperationFilters
{
	public class OpenAPIOperationsSecurityFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			bool existsAuthorizeAttribute = context.ApiDescription.CustomAttributes()
				.Any(Attribute => Attribute.GetType() == typeof(AuthorizeAttribute));

			bool existsAllowAnonymousAttribute = context.ApiDescription.CustomAttributes()
				.Any(Attribute => Attribute.GetType() == typeof(AllowAnonymousAttribute));
			;
			if (!existsAuthorizeAttribute || existsAllowAnonymousAttribute)
			{
				operation.Security = new List<OpenApiSecurityRequirement>
				{
					new OpenApiSecurityRequirement()
				};
				//set: ""security": [{ }]"
				return;
			}
		}
	}
}
