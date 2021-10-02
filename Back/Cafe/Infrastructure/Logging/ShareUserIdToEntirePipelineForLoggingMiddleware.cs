using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Model.Shared;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.Logging
{
	public class ShareUserIdToEntirePipelineForLoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly AppSettings _appSettings;
		public ShareUserIdToEntirePipelineForLoggingMiddleware(RequestDelegate next,
			AppSettings appSettings)
		{
			_next = next;
			_appSettings = appSettings;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			IDisposable userIdentifierLogContext = null;
			string userIdConst = _appSettings.Constants.LoggingParams.UserIdentifier;

			if (!context.User.Claims.Any())
			{
				userIdentifierLogContext = LogContext.PushProperty(userIdConst,
					$"anon, ip=[{context.Connection.RemoteIpAddress.ToString().Replace("::1", "localhost").Replace(':', '.')}]");
			}
			else
			{
				userIdentifierLogContext =
					new UserService(_appSettings).TryGetUserIdAndUserName(context.User.Claims,
						out string userIdAndUserName) ?
							LogContext.PushProperty(userIdConst, userIdAndUserName) :
							throw new InvalidOperationException(
								$"In {nameof(ShareUserIdToEntirePipelineForLoggingMiddleware)}," +
								$"{userIdConst} is not found, bad cookie?");
			}

			using (userIdentifierLogContext)
			{
				await _next.Invoke(context);
			}
			return;
		}
	}
}
