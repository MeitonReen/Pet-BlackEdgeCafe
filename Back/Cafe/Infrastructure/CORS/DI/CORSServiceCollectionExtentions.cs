using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cafe.Infrastructure.CORS.DI
{
	public static class CORSServiceCollectionExtentions
	{
		public static void AddCafeCORSDevelopment(this IServiceCollection services,
				AppSettings appSettings)
		{
			services.AddCors(Options =>
				{
					Options.AddPolicy(appSettings.Constants.CorsPolicies.Dev,
						CreateConfiguratorPolicyCORSDevelopment(appSettings));
				});
		}
		private static Action<CorsPolicyBuilder> CreateConfiguratorPolicyCORSDevelopment(
			AppSettings appSettings)
		{
			return new Action<CorsPolicyBuilder>(policyBuider => policyBuider
				.WithOrigins("http://localhost:3000")
				.AllowCredentials()
				.WithMethods(HttpMethods.Get, HttpMethods.Post, HttpMethods.Put,
					HttpMethods.Patch, HttpMethods.Options, HttpMethods.Delete)
				.WithHeaders(appSettings.Constants.AntiforgeryTokenRequestHeaderName)
				.WithExposedHeaders(appSettings.Constants.AntiforgeryTokenResponseHeaderName));
		}
	}
}
