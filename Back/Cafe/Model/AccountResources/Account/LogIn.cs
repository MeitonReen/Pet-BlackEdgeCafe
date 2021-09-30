
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.HandlersChain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cafe.Model.AccountResources.Account
{
	public class LogIn : HandlerBase
	{
		private readonly AppSettings _appSettings = null;
		private readonly HttpContext _httpContext = null;

		#region params from chain request
		private User user = null;
		#endregion

		public LogIn(AppSettings appSettings, HttpContext httpContext)
		{
			_appSettings = appSettings;
			_httpContext = httpContext;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			user = GetParamFromChainRequest<User>(
				this.GetType().Name, request, nameof(user));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest chainRequest)
		{
			var claims = new List<Claim>
			{
				new Claim(_appSettings.Constants.UserId, user.Id.ToString()),
				new Claim(_appSettings.Constants.UserName, user.UserName)
			};
			ClaimsIdentity id = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(id));

			chainRequest.Result = _resultGenerator.Ok();
			chainRequest.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}