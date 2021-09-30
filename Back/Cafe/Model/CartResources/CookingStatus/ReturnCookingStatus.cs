using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.CookingStatus
{
	public class ReturnCookingStatus : HandlerBase
	{
		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		#endregion

		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				GetType().Name, request, nameof(clientCart));
			return;
		}
		protected override Task ExecuteAsync(ChainRequest request)
		{
			request.Result = _resultGenerator.Ok(new CookingStatusDTO(clientCart
				.CookingStatus));
			request.Status = ChainProcessingStatus.Success;

			return Task.CompletedTask;
		}
	}
}