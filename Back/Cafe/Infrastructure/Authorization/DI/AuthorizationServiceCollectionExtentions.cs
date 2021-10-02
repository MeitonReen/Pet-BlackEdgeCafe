using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.Authorization.Policies.Default.Handlers;
using Cafe.Infrastructure.Authorization.Policies.Default.Requirements;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Cafe.Infrastructure.Authorization.DI
{
	public static class AuthorizationServiceCollectionExtentions
	{
		public static void AddCafeAuthorizationDefault(this IServiceCollection services,
				AppSettings appSettings)
		{
			services.AddAuthorization(conf =>
				conf.DefaultPolicy = CreateDefaultAuthorizationPolicy(appSettings)
						.Build()
			);
			services.AddScoped<IAuthorizationHandler, UserIsExistsHandler>();
		}
		private static AuthorizationPolicyBuilder CreateDefaultAuthorizationPolicy(
			AppSettings appSettings)
		{
			return new AuthorizationPolicyBuilder(CookieAuthenticationDefaults
					.AuthenticationScheme)
						.RequireClaim(appSettings.Constants.UserId)
						.RequireClaim(appSettings.Constants.UserName)
						.AddRequirements(new UserIsExists());
		}
	}
}
