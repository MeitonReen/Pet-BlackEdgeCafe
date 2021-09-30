
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cafe.Model.AccountResources.Account
{
	public class Logout : HandlerBase
	{
		private readonly HttpContext _httpContext = null;

		public Logout(HttpContext httpContext)
		{
			_httpContext = httpContext;
		}
		protected async override Task ExecuteAsync(ChainRequest chainRequest)
		{
			await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			chainRequest.Result = _resultGenerator.Ok();
			chainRequest.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}