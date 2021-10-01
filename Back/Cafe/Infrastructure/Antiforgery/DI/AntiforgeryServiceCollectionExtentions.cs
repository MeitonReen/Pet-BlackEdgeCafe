using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cafe.Infrastructure.Authentication.DI
{
	public static class AntiforgeryServiceCollectionExtentions
	{
		public static void AddCafeAntiforgery(this IServiceCollection services,
				AppSettings appSettings)
		{
			services.AddAntiforgery(CreateConfiguratorAntiforgery(appSettings));
		}
		private static Action<AntiforgeryOptions> CreateConfiguratorAntiforgery(AppSettings
			appSettings)
		{
			return new(options =>
			{
				options.HeaderName = appSettings.Constants.AntiforgeryTokenRequestHeaderName;
				options.Cookie.Name = appSettings.Constants.AntiforgeryTokenCookieName;
				options.Cookie.SameSite = SameSiteMode.None;
				options.Cookie.HttpOnly = true;
				options.Cookie.Path = "/";
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
			});
		}
	}
}
