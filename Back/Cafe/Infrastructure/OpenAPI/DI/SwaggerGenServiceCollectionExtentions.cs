using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.OpenAPI.OperationFilters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace Cafe.Infrastructure.DI
{
	public static class SwaggerGenServiceCollectionExtentions
	{
		private static AppSettings _appSettings = null;
		public static void AddSwaggerGenForCafeAPIv1(this IServiceCollection services,
			AppSettings appSettings)
		{
			_appSettings = appSettings;
			services.AddSwaggerGen(OptionsActionV1);
		}
		private static void OptionsActionV1(SwaggerGenOptions options)
		{
			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "CafeAPI",
				Version = "1.0.0"
			});
			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

			options.IncludeXmlComments(xmlPath);

			options.AddServer(new OpenApiServer
			{
				Url = _appSettings.Constants.ServerUrlOpenAPI
			});
			;
			var SecurityScheme = new OpenApiSecurityScheme()
			{
				Type = SecuritySchemeType.ApiKey,
				Name = _appSettings.Constants.AuthCookieName,
				In = ParameterLocation.Cookie,
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = _appSettings.Constants.AuthCookieName
				}
			};
			OpenApiSecurityRequirement SecurityRequirement = new()
			{
				{ SecurityScheme, Array.Empty<string>() }
			};

			options.OperationFilter<OpenAPIInternalServerError>();
			options.OperationFilter<OpenAPIUnauthorized>();
			options.OperationFilter<OpenAPIOperationsSecurityFilter>();
			options.OperationFilter<OpenAPIDefineSetCookieHeaderFilter>();
			options.OperationFilter<OpenAPIDefineAntiforgeryTokenHeaderFilter>();
			options.OperationFilter<OpenAPIETagCache>();

			options.AddSecurityDefinition(SecurityScheme.Reference.Id, SecurityScheme);
			options.AddSecurityRequirement(SecurityRequirement);

			options.CustomOperationIds(MethodDescription =>
			{
					//"Cafe.Controllers.Account.AccountController.Login (Cafe)"
					//<-- operationid == login
					string ActionMethodName = MethodDescription.ActionDescriptor.DisplayName;

				string operationid = ActionMethodName[(ActionMethodName.LastIndexOf('.') + 1)..];
				operationid = operationid.Substring(0, operationid.LastIndexOf(' '));

				operationid = char.ToLowerInvariant(operationid[0]) + operationid[1..];

				return operationid;
			});

			//Groupoing by root resource
			//Examples:
			//api/v1/orders/orders-on-tables
			//<-- Orders
			//api/v1/menu/dishes/details/{dishid}
			//<-- Menu
			options.TagActionsBy(api =>
			{
					//api/v1/rootResource/...
					//<-- tagsForOperation[] { rootResource }
					string rootResource = api.RelativePath.Replace("api/v1/", "");
				int lenghtSubstr = rootResource.IndexOf('/');
				lenghtSubstr = lenghtSubstr != -1 ? lenghtSubstr : rootResource.Length;

				rootResource = rootResource.Substring(0, lenghtSubstr);

				rootResource = char.ToUpperInvariant(rootResource[0]) + rootResource[1..];

				return new string[] { rootResource };
			});
		}
	}
}
