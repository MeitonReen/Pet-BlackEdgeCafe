using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Model.Shared.AuthorizationPolicies.Default.CustomRequirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.Shared.AuthorizationPolicies.Default.CustomHandlers
{
	public class UserIsExistsHandler : AuthorizationHandler<UserIsExists>
	{
		private readonly UserManager<User> _userManager = null;
		private readonly AppSettings _appSettings = null;
		private readonly UserService _userService = null;

		public UserIsExistsHandler(UserManager<User> userManager, AppSettings appSettings)
		{
			_userManager = userManager;
			_appSettings = appSettings;
			_userService = new(_appSettings);
		}
		protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context,
			UserIsExists requirement)
		{
			if (!_userService.TryGetUserId(context.User.Claims, out var userId))
			{
				context.Fail();
				return;
			}

			if (await _userManager.FindByIdAsync(userId.ToString()) == null)
			{
				context.Fail();
				return;
			}

			context.Succeed(requirement);
			return;
		}
	}
}