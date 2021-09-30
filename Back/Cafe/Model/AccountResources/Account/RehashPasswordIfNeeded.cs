using Cafe.Databases.Identity.Contexts.Interfaces;
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.HandlersChain;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Cafe.Model.AccountResources.Account
{
	public class RehashPasswordIfNeeded : HandlerBase
	{
		private readonly IdentityDatabase _usersDB = null;
		private readonly string _password = string.Empty;
		private readonly IPasswordHasher<User> _passwordHasher = null;

		#region params from chain request
		private User user = null;
		private PasswordVerificationResult verifyResult = PasswordVerificationResult.Failed;
		#endregion

		public RehashPasswordIfNeeded(IdentityDatabase usersDB, string password,
			IPasswordHasher<User> passwordHasher)
		{
			_usersDB = usersDB;
			_password = password;
			_passwordHasher = passwordHasher;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			string className = this.GetType().Name;

			user = GetParamFromChainRequest<User>(
				className, request, nameof(user));
			verifyResult = GetParamFromChainRequest<PasswordVerificationResult>(
				className, request, nameof(verifyResult));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			if (verifyResult == PasswordVerificationResult.SuccessRehashNeeded)
			{
				user.PasswordHash = _passwordHasher.HashPassword(user, _password);
				await _usersDB.SaveChangesAsync();
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}