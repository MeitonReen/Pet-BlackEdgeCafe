using Cafe.Databases.Cafe.Context.Implementations;
using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Identity.Contexts.Implementations;
using Cafe.Databases.Identity.Contexts.Interfaces;
using Cafe.Databases.Identity.DI;
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.Authentication.DI;
using Cafe.Infrastructure.Authorization.DI;
using Cafe.Infrastructure.DI;
using Cafe.Infrastructure.EFCore.DI;
using Cafe.Infrastructure.ETagCache.Databases.Contexts.Implementations;
using Cafe.Infrastructure.ETagCache.DI;
using Cafe.Infrastructure.OpenAPI;
using Cafe.Infrastructure.OpenAPI.OperationFilters;
using Cafe.Model.DTOs;
using Cafe.Model.Shared;
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
			services.AddCafeCookieAuthentication(_appSettings);

			services.AddCafeAuthorizationDefault(_appSettings);
			#endregion

			services.AddControllers();

			services.AddSwaggerGenForCafeAPIv1(_appSettings);

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
