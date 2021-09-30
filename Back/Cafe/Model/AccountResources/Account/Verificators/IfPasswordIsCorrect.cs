using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Cafe.Model.AccountResources.Account.Verificators
{
	public class IfPasswordIsCorrect : HandlerBase
	{
		private readonly string _password = string.Empty;
		private readonly IPasswordHasher<User> _passwordHasher = null;

		#region params from chain request
		private User user = null;
		#endregion

		#region params to chain request
		private PasswordVerificationResult verifyResult = PasswordVerificationResult.Failed;
		#endregion

		public IfPasswordIsCorrect(string password, IPasswordHasher<User> passwordHasher)
		{
			_password = password;
			_passwordHasher = passwordHasher;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			user = GetParamFromChainRequest<User>(this.GetType().Name, request, nameof(user));
			return;
		}
		protected override Task ExecuteAsync(ChainRequest request)
		{
			verifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash,
				_password);
			if (verifyResult == PasswordVerificationResult.Failed)
			{
				request.Status = ChainProcessingStatus.Failure_exit;
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Bad login or password"));
				return Task.CompletedTask;
			}
			request.Status = ChainProcessingStatus.Success;
			return Task.CompletedTask;
		}
		protected override void SetParamsToChainRequest(ChainRequest request)
		{
			request.Context.Add(nameof(verifyResult), verifyResult);
			return;
		}
	}
}