using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Model.DTOs;
using Cafe.Model.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cafe.Infrastructure.Authentication
{
	public static class AuthenticationServiceCollectionExtentions
	{
		public static void AddCafeCookieAuthentication(this IServiceCollection services,
				AppSettings appSettings)
		{
			CookieBuilder defaultAuthCookieBuilder = CreateDefaultAuthCookieBuilder(appSettings);

			services.AddSingleton(defaultAuthCookieBuilder);

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(CreateConfiguratorDefaultAuthCookieOptions(defaultAuthCookieBuilder));
		}
		private static CookieBuilder CreateDefaultAuthCookieBuilder(AppSettings appSettings)
		{
			return new()
			{
				Name = appSettings.Constants.AuthCookieName,
				SameSite = SameSiteMode.Lax,
				HttpOnly = true,
				Path = "/",
				SecurePolicy = CookieSecurePolicy.Always,
				MaxAge = new TimeSpan(3, 0, 0, 0)
			};
		}
		private static Action<CookieAuthenticationOptions> CreateConfiguratorDefaultAuthCookieOptions(
			CookieBuilder defaultAuthCookieBuilder)
		{
			return new Action<CookieAuthenticationOptions>(options =>
			{
				options.Cookie = defaultAuthCookieBuilder;
				options.Events.OnRedirectToLogin = context =>//Not authentificated, authorization denied
				{
					context.Response.StatusCode = StatusCodes.Status401Unauthorized;

					return context.Response.WriteAsJsonAsync(new ErrorDTO("Access denied"));
				};
				options.Events.OnRedirectToAccessDenied = context =>//Authentificated, authorization denied
				{
					UserService.UnsetCookie(context.Response, defaultAuthCookieBuilder.Name,
						defaultAuthCookieBuilder.BuildFromSelf());

					context.Response.StatusCode = StatusCodes.Status401Unauthorized;
					return context.Response.WriteAsJsonAsync(new ErrorDTO("Bad cookie"));
				};
			});
		}
	}
}
