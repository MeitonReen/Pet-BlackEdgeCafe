using Cafe.Databases.Identity.Contexts.Interfaces;
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Controllers.Account
{
	[ApiController]
	[Route(CafeAPIRoutes.V1.This)]
	[Produces("application/json")]
	public partial class AccountController : ControllerBase
	{
		private readonly IPasswordHasher<User> _passwordHasher = null;
		private readonly UserManager<User> _userManager = null;
		private readonly IdentityDatabase _usersDB = null;
		private readonly AppSettings _appSettings = null;
		public AccountController(IPasswordHasher<User> passwordHasher, UserManager<User> userManager,
			IdentityDatabase usersDB, AppSettings appSettings)
		{
			_passwordHasher = passwordHasher;
			_userManager = userManager;
			_usersDB = usersDB;
			_appSettings = appSettings;
		}
	}
}
