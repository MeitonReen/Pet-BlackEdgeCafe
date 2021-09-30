using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Cafe.Infrastructure.OpenApiFilters
{
	public class OpenAPIDefineAntiforgeryTokenHeaderFilter : IOperationFilter
	{
		private readonly AppSettings _appSettings = null;

		public OpenAPIDefineAntiforgeryTokenHeaderFilter(AppSettings appSettings)
		{
			_appSettings = appSettings;
		}
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			bool existsValidateAntiForgeryTokenAttribute = context.ApiDescription
				.CustomAttributes()
				.Any(Attribute => Attribute.GetType() == typeof(
					ValidateAntiForgeryTokenAttribute));

			bool existsIgnoreAntiforgeryTokenAttribute = context.ApiDescription
				.CustomAttributes()
				.Any(Attribute => Attribute.GetType() == typeof(
					IgnoreAntiforgeryTokenAttribute));

			if (existsValidateAntiForgeryTokenAttribute &&
				!existsIgnoreAntiforgeryTokenAttribute)
			{
				/*operation.Parameters.Add(new OpenApiParameter()
				{
					In = ParameterLocation.Cookie,
					Name = _appSettings.Constants.AntiforgeryTokenCookieName,
					Schema = new OpenApiSchema()
					{
						Type = _appSettings.Constants.Str
					},
					Example = new OpenApiString("Cookie: " + _appSettings.Constants
						.AntiforgeryTokenCookieName + "=\"some token\""),
					Required = true
				});*/

				/*
				set to Parameters from Operation from Paths:

				"in": "cookie",
				"name": "CSRF-Token-Cookie",
				"schema": {
					"type": "string",
				},
				"example": "Cookie: CSRF-Token-Cookie="some token"",
				"required": true
				*/
				operation.Parameters.Add(new OpenApiParameter()
				{
					In = ParameterLocation.Header,
					Name = _appSettings.Constants.AntiforgeryTokenRequestHeaderName,
					Schema = new OpenApiSchema()
					{
						Type = typeof(string).Name.ToLower()
					},
					Example = new OpenApiString(_appSettings.Constants
						.AntiforgeryTokenRequestHeaderName + ": \"some token\""),
					Required = true
				});
				/*
				set to Parameters from operation from Paths:

				"in": "header",
				"name": "X-CSRF-Token-Request",
				"schema": {
					"type": "string",
				},
				"example": "X-CSRF-Token-Request: "some token"",
				"required": true
				*/
				return;
			}
			return;
		}
	}
}
