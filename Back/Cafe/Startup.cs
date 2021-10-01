using Cafe.Databases.Cafe.Context.Implementations;
using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Identity.Contexts.Implementations;
using Cafe.Databases.Identity.Contexts.Interfaces;
using Cafe.Databases.Identity.DI;
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.DI;
using Cafe.Infrastructure.ETagCache.Databases.Contexts.Implementations;
using Cafe.Infrastructure.ETagCache.DI;
using Cafe.Infrastructure.OpenAPI;
using Cafe.Infrastructure.OpenAPI.OperationFilters;
using Cafe.Model.DTOs;
using Cafe.Model.Shared;
using Cafe.Model.Shared.AuthorizationPolicies.Default.CustomHandlers;
using Cafe.Model.Shared.AuthorizationPolicies.Default.CustomRequirements;
using Cafe.Model.Shared.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace Cafe
{
	public class Startup
	{
		private readonly AppSettings _appSettings = new();
		private readonly IWebHostEnvironment _env = null;
		public Startup(IConfiguration appSettingsConf, IWebHostEnvironment env)
		{
			_appSettings = appSettingsConf.Get<AppSettings>();
			_env = env;
			RunScripts();
		}
		private async void RunScripts()
		{
			await new OpenAPIGenerator(_appSettings).GenerateDefaultAsync();
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<AppSettings>(_appSettings);

			services.AddSpaStaticFiles(conf =>
				conf.RootPath = _appSettings.Constants.RootSpaStaticFilesPath);

			services.AddETagCache<ETagCacheSqliteContext>();

			services.AddDbContextScopedAsT<CafeSqliteContext, CafeDatabase>();

			#region IdentityCoreWithEFCore
			services.AddIdentityEFCoreScopedAsT<User, IdentitySqliteContext, IdentityDatabase>(
				indentityOpts =>
			{
				indentityOpts.Password.RequireDigit = false;
				indentityOpts.Password.RequireNonAlphanumeric = false;
				indentityOpts.Password.RequireUppercase = false;
			});
			#endregion

			#region Authentification & Authorization
			CookieBuilder authCookieBuilder = new()
			{
				Name = _appSettings.Constants.AuthCookieName,
				SameSite = SameSiteMode.Lax,
				HttpOnly = true,
				Path = "/",
				SecurePolicy = CookieSecurePolicy.Always,
				MaxAge = new TimeSpan(3, 0, 0, 0)
			};
			services.AddSingleton<CookieBuilder>(authCookieBuilder);

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.Cookie = authCookieBuilder;
					options.Events.OnRedirectToLogin = context =>//Not authentificated, authorization denied
					{
						context.Response.StatusCode = StatusCodes.Status401Unauthorized;

						return context.Response.WriteAsJsonAsync(new ErrorDTO("Access denied"));
					};
					options.Events.OnRedirectToAccessDenied = context =>//Authentificated, authorization denied
					{
						UserService.UnsetCookie(context.Response, authCookieBuilder.Name,
							authCookieBuilder.BuildFromSelf());

						context.Response.StatusCode = StatusCodes.Status401Unauthorized;
						return context.Response.WriteAsJsonAsync(new ErrorDTO("Bad cookie"));
					};
				});
			services.AddAuthorization(conf =>
				conf.DefaultPolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults
					.AuthenticationScheme)
						.RequireClaim(_appSettings.Constants.UserId)
						.RequireClaim(_appSettings.Constants.UserName)
						.AddRequirements(new UserIsExists())
						.Build()
			);

			services.AddScoped<IAuthorizationHandler, UserIsExistsHandler>();//Use EFCore -> Scoped
			#endregion

			services.AddControllers();

			#region OpenAPI
			services.AddSwaggerGen(setup =>
			{
				setup.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "CafeAPI",
					Version = "1.0.0"
				});
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

				setup.IncludeXmlComments(xmlPath);

				setup.AddServer(new OpenApiServer
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

				setup.OperationFilter<OpenAPIInternalServerError>();
				setup.OperationFilter<OpenAPIUnauthorized>();
				setup.OperationFilter<OpenAPIOperationsSecurityFilter>();
				setup.OperationFilter<OpenAPIDefineSetCookieHeaderFilter>();
				setup.OperationFilter<OpenAPIDefineAntiforgeryTokenHeaderFilter>();
				setup.OperationFilter<OpenAPIETagCache>();

				setup.AddSecurityDefinition(SecurityScheme.Reference.Id, SecurityScheme);
				setup.AddSecurityRequirement(SecurityRequirement);

				setup.CustomOperationIds(MethodDescription =>
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
				setup.TagActionsBy(api =>
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
			});
			#endregion

			#region EFCoreProfiler
			services.AddMemoryCache();
			services
				.AddMiniProfiler(options => options.RouteBasePath = "/profiler")
				.AddEntityFramework();
			#endregion

			services.AddControllersWithViews();

			#region Cors
			if (_env.IsDevelopment())
			{
				services.AddCors(Options =>
				{
					Options.AddPolicy(_appSettings.Constants.CorsPolicies.Dev, Builder => Builder
						.WithOrigins("http://localhost:3000")
						.AllowCredentials()
						.WithMethods(HttpMethods.Get, HttpMethods.Post, HttpMethods.Put,
							HttpMethods.Patch, HttpMethods.Options, HttpMethods.Delete)
						.WithHeaders(_appSettings.Constants.AntiforgeryTokenRequestHeaderName)
						.WithExposedHeaders(_appSettings.Constants
							.AntiforgeryTokenResponseHeaderName));
				});
			}
			#endregion

			#region Antiforgery
			/*services.AddAntiforgery(setup =>
			{
				//�� ������ ��������� ������� ����� ���� ����� ��� ���������
				setup.HeaderName = _appSettings.Constants.AntiforgeryTokenRequestHeaderName;
				//�� ������ ���� ������� ����� ������ ����� ��� ���������
				//� ��� �������� ���� ��� ������ ��� GetAndStoreTokens
				setup.Cookie.Name = _appSettings.Constants.AntiforgeryTokenCookieName;
				setup.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
				setup.Cookie.HttpOnly = true;
				setup.Cookie.Path = "/";
				setup.Cookie.SecurePolicy = CookieSecurePolicy.Always;
			});*/
			#endregion

			services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.ForwardedHeaders =
					ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
				options.KnownProxies.Add(IPAddress.Parse("172.17.0.3"));
			});
		}
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseForwardedHeaders();

			app.UseAuthentication();//fill claims

			app.UseShareUserIdForLoggingToEntirePipeline();//share userId from claims for logging

			app.UseSerilogRequestLogging();

			if (env.IsDevelopment())
			{
				app.UseExceptionHandler("/error");
				//app.UseHttpsRedirection();
			};

			app.UseDefaultFiles();
			app.UseSpaStaticFiles();

			app.UseRouting();

			if (env.IsDevelopment())
			{
				app.UseCors(_appSettings.Constants.CorsPolicies.Dev);
			}

			app.UseAuthorization();

			if (env.IsDevelopment())
			{
				app.UseMiniProfiler();
			}

			app.UseSwagger();
			app.UseSwaggerUI(setup => setup.SwaggerEndpoint("v1/swagger.json", "Cafe v1"));

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSpa(conf =>
			{
				conf.Options.DefaultPage = new PathString(_appSettings.Constants.DefaultPage);

				/*if (env.IsDevelopment())
				{
					conf.UseProxyToSpaDevelopmentServer("http://localhost:3000");
				}*/
			});
		}
	}
}
