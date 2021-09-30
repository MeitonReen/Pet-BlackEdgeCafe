using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.HandlersChain;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cafe.Model.Shared.Processing
{
	public class UserIdToContext : HandlerBase
	{
		private readonly AppSettings _appSettings = null;
		private readonly ClaimsPrincipal _user = null;
		private readonly UserService _userService = null;

		#region params to chain request
		private Guid userId = Guid.Empty;
		#endregion

		public UserIdToContext(AppSettings appSettings, ClaimsPrincipal user)
		{
			_appSettings = appSettings;
			_user = user;
			_userService = new(_appSettings);
		}
		protected override Task ExecuteAsync(ChainRequest chainRequest)
		{
			if (!_userService.TryGetUserId(_user.Claims, out userId))
			{
				throw new ArgumentNullException(nameof(userId), "The userId's existence was verified earlier, check policy");
			};

			chainRequest.Status = ChainProcessingStatus.Success;
			return Task.CompletedTask;
		}
		protected override void SetParamsToChainRequest(ChainRequest request)
		{
			request.Context.Add(nameof(userId), userId);
			return;
		}
	}
}