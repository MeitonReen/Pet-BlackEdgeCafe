using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using System.Threading.Tasks;

namespace Cafe.Model.AccountResources.Account.Verificators
{
	public class IfPasswordAndConfPasswordMatched : HandlerBase
	{
		private readonly string _password = string.Empty;
		private readonly string _confirmPassword = string.Empty;

		public IfPasswordAndConfPasswordMatched(string password, string confirmPassword)
		{
			_password = password;
			_confirmPassword = confirmPassword;
		}
		protected override Task ExecuteAsync(ChainRequest request)
		{
			if (_password != _confirmPassword)
			{
				request.Status = ChainProcessingStatus.Failure_exit;
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Пароли не совпадают"));
				return Task.CompletedTask;
			}
			request.Status = ChainProcessingStatus.Success;
			return Task.CompletedTask;
		}
	}
}