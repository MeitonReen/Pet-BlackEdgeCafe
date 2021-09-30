using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Cafe.Infrastructure.OpenApiFilters
{
	public class OpenAPIDefineSetCookieHeaderFilter : IOperationFilter
	{
		private readonly CookieBuilder _authCookieBuilder = null;

		public OpenAPIDefineSetCookieHeaderFilter(CookieBuilder authCookieBuilder)
		{
			_authCookieBuilder = authCookieBuilder;
		}
		private string GenerateCookieParams()
		{
			DefaultHttpContext defaultHttpContext = new();
			defaultHttpContext.Response.Cookies.Append(_authCookieBuilder.Name, "",
				_authCookieBuilder.BuildFromSelf());

			string generatedCookie = defaultHttpContext.Response.Headers[HeaderNames.SetCookie];
			string paramsGeneratedCookie = generatedCookie.Replace(_authCookieBuilder.Name + "=; ", "");
			;
			return paramsGeneratedCookie;
		}
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (operation.OperationId == "login")
			{
				string ok = StatusCodes.Status200OK.ToString();
				KeyValuePair<string, OpenApiResponse> response = operation.Responses
					.SingleOrDefault(Response => Response.Key == ok);

				if (response.Value == null)
				{
					return;
				}

				response.Value.Headers.Add(HeaderNames.SetCookie,
					new OpenApiHeader()
					{
						Schema = new OpenApiSchema()
						{
							Type = typeof(string).Name.ToLower(),
							Example = new OpenApiString(_authCookieBuilder.Name +
								"=\"some encrypted data\"; " + GenerateCookieParams())
						},
						Required = true
					});
				/*example settings set:
				 "headers": {
					"Set-Cookie": {
						"required": true,
						"schema": {
							"type": "string",
							"example": "AuthCookie=\"some encrypted data\"; path=/; secure; samesite=lax; httponly"
						}
					}
				},*/
				return;
			}
			return;
		}
	}
}
