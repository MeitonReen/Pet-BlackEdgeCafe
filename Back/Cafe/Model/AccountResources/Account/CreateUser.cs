using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.HandlersChain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.AccountResources.Account
{
	public class CreateUser : HandlerBase
	{
		private readonly string _login = string.Empty;
		private readonly string _password = string.Empty;
		private readonly UserManager<User> _userManager = null;

		public CreateUser(string login, string password, UserManager<User> userManager)
		{
			_login = login;
			_password = password;
			_userManager = userManager;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			User user = new() { UserName = _login };
			IdentityResult res = await _userManager.CreateAsync(user, _password);

			if (!res.Succeeded)
			{
				throw new Exception(res.Errors.Select(Error => Error.Code)
					.Aggregate((Acc, ErrorCode) => Acc += ErrorCode + ';'));
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}