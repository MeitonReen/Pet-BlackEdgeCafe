using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Cafe.Model.AccountResources.Account.Verificators
{
	public class IfLoginIsNotOccupied : HandlerBase
	{
		private readonly string _login = string.Empty;
		private readonly UserManager<User> _userManager = null;

		public IfLoginIsNotOccupied(string login, UserManager<User> userManager)
		{
			_login = login;
			_userManager = userManager;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			User user = await _userManager.FindByNameAsync(_login);

			if (user != default(User))
			{
				request.Status = ChainProcessingStatus.Failure_exit;
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Этот логин занят"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}