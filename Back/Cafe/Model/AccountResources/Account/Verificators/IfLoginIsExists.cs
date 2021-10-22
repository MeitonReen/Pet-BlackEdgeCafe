using Cafe.Databases.Identity.Contexts.Interfaces;
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cafe.Model.AccountResources.Account.Verificators
{
	public class IfLoginIsExists : HandlerBase
	{
		private readonly IdentityDatabase _usersDB = null;
		private readonly string _login = string.Empty;

		#region params to chain request
		private User user = null;
		#endregion
		public IfLoginIsExists(IdentityDatabase usersDB, string login)
		{
			_usersDB = usersDB;
			_login = login;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			user = await _usersDB.Users.SingleOrDefaultAsync(User => User.UserName == _login);

			if (user == default(User))
			{
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Bad login or password"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
		protected override void SetParamsToChainRequest(ChainRequest request)
		{
			request.Context.Add(nameof(user), user);
			return;
		}
	}
}