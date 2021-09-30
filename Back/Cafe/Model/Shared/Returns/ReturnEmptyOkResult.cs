using Cafe.Infrastructure.HandlersChain;
using System.Threading.Tasks;

namespace Cafe.Model.Shared.Returns
{
	public class ReturnEmptyOkResult : HandlerBase
	{
		protected override Task ExecuteAsync(ChainRequest request)
		{
			request.Result = _resultGenerator.Ok();
			request.Status = ChainProcessingStatus.Success;

			return Task.CompletedTask;
		}
	}
}