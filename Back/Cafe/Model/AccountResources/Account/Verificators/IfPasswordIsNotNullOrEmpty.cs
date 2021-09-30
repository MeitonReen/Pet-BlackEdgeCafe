using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using System.Threading.Tasks;

namespace Cafe.Model.AccountResources.Account.Verificators
{
	public class IfPasswordIsNotNullOrEmpty : HandlerBase
	{
		private readonly string _password = string.Empty;

		public IfPasswordIsNotNullOrEmpty(string password)
		{
			_password = password;
		}
		protected override Task ExecuteAsync(ChainRequest request)
		{
			if (string.IsNullOrEmpty(_password))
			{
				request.Status = ChainProcessingStatus.Failure_exit;
				request.Result = _resultGenerator.BadRequest(new ErrorDTO(
					"Пароль должен содержать не менее 5 символов"));
				return Task.CompletedTask;
			}
			request.Status = ChainProcessingStatus.Success;
			return Task.CompletedTask;
		}
	}
}