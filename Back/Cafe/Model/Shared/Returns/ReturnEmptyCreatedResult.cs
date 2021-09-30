using Cafe.Infrastructure.HandlersChain;
using System.Threading.Tasks;

namespace Cafe.Model.Shared.Returns
{
	public class ReturnEmptyCreatedResult : HandlerBase
	{
		protected override Task ExecuteAsync(ChainRequest request)
		{
			request.Result = _resultGenerator.Created(string.Empty, null);
			request.Status = ChainProcessingStatus.Success;

			return Task.CompletedTask;
		}
	}
}