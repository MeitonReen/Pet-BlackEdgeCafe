using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cafe.Model.Shared.Verificators
{
	public class IfUserClaimsIsCorrect : HandlerBase
	{
		private readonly AppSettings _appSettings = null;
		private readonly HttpContext _httpContext = null;
		private readonly CookieOptions _authCookieOptions = null;

		#region params to chain request
		private Guid userId = Guid.Empty;
		#endregion

		public IfUserClaimsIsCorrect(AppSettings appSettings, HttpContext httpContext,
			CookieOptions authCookieOptions)
		{
			_appSettings = appSettings;
			_httpContext = httpContext;
			_authCookieOptions = authCookieOptions;
		}
		protected override Task ExecuteAsync(ChainRequest chainRequest)
		{
			Guid.TryParse(_httpContext.User.Claims.SingleOrDefault(Claim =>
				Claim.Type == _appSettings.Constants.UserId)?.Value, out userId);

			string userName = _httpContext.User.Claims.SingleOrDefault(Claim =>
				Claim.Type == _appSettings.Constants.UserName)?.Value;

			if (!UserIsCorrect(userId, userName))
			{
				Return401UnsetCookie(chainRequest);
				return Task.CompletedTask;
			};

			chainRequest.Status = ChainProcessingStatus.Success;
			return Task.CompletedTask;
		}
		protected override void SetParamsToChainRequest(ChainRequest request)
		{
			request.Context.Add(nameof(userId), userId);
			return;
		}
		private static bool UserIsCorrect(in Guid userId, string userName)
		{
			return (userId != Guid.Empty && userName != null);
		}
		private void Return401UnsetCookie(ChainRequest chainRequest)
		{
			chainRequest.Status = ChainProcessingStatus.Failure_exit;
			chainRequest.Result = _resultGenerator.Unauthorized(new ErrorDTO("Bad cookie"));

			_authCookieOptions.MaxAge = null;
			_authCookieOptions.Expires = DateTimeOffset.Now.AddSeconds(-1);

			_httpContext.Response.Cookies.Append(_appSettings.Constants.AuthCookieName,
				string.Empty, _authCookieOptions);
		}
	}
}