using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.AccountResources.Account;
using Cafe.Model.AccountResources.Account.Verificators;
using Cafe.Model.DTOs;
using Cafe.Model.Shared.Returns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Cafe.Infrastructure;

namespace Cafe.Controllers.Account
{
	public partial class AccountController : ControllerBase
	{
		/// <summary>Login</summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		/// <response code="400">Bad login or password</response>
		/// <response code="200">Return Set-Cookie</response>
		[HttpPost]
		[Route(CafeAPIRoutes.V1.Account.Login)]
		[Consumes(MimeTypes.Application.XWWWFormUrlencoded)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
		public async Task<IActionResult> Login([FromForm][Required] string login,
			[FromForm][Required] string password)
		{
			return await new HandlersChain()
				.AddChainLink(() => new IfLoginIsExists(_usersDB, login))
				.AddChainLink(() => new IfPasswordIsCorrect(password, _passwordHasher))
				.AddChainLink(() => new RehashPasswordIfNeeded(_usersDB, password,
					_passwordHasher))
				.AddChainLink(() => new LogIn(_appSettings, HttpContext))
				.RunChainAsync();
		}
		/// <summary>
		/// Logout
		/// </summary>
		/// <response code="200">Return Set-Cookie</response>
		[Authorize]
		[HttpPost]
		[Route(CafeAPIRoutes.V1.Account.Logout)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
		public async Task<IActionResult> Logout()
		{
			return await new HandlersChain()
				.AddChainLink(() => new Logout(HttpContext))
				.RunChainAsync();
		}
		/// <summary>
		/// Check login
		/// </summary>
		/// <response code="200"></response>
		[Authorize]
		[HttpGet]
		[Route(CafeAPIRoutes.V1.Account.Login)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
		public IActionResult CheckLogin()
		{
			return Ok();
		}
		/// <summary>
		/// Registration
		/// </summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		/// <param name="confirmPassword"></param>
		/// <response code="400">Password and passwordConfirm don't match or is null/empty;
		///	Login is taken</response>
		/// <response code="200"></response>
		[HttpPost]
		[Route(CafeAPIRoutes.V1.Account.Registration)]
		[Consumes(MimeTypes.Application.XWWWFormUrlencoded)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
		public async Task<IActionResult> Registration([FromForm][Required] string login,
			[FromForm][Required] string password,
			[FromForm][Required] string confirmPassword)
		{
			return await new HandlersChain()
				.AddChainLink(() => new IfPasswordIsNotNullOrEmpty(password))
				.AddChainLink(() => new IfPasswordAndConfPasswordMatched(password,
					confirmPassword))
				.AddChainLink(() => new IfLoginIsNotOccupied(login, _userManager))
				.AddChainLink(() => new CreateUser(login, password, _userManager))
				.AddChainLink(() => new ReturnEmptyOkResult())
				.RunChainAsync();
		}
	}
}
