using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.OrdersResources.Orders
{
	public class ReturnNewOrderId : HandlerBase
	{
		#region params from chain request
		private Guid newOrderId = Guid.Empty;
		#endregion

		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			newOrderId = GetParamFromChainRequest<Guid>(this.GetType().Name, request,
				nameof(newOrderId));
			return;
		}
		protected override Task ExecuteAsync(ChainRequest request)
		{
			request.Result = _resultGenerator.Created(string.Empty,
				new OrderIdDTO(newOrderId));
			request.Status = ChainProcessingStatus.Success;

			return Task.CompletedTask;
		}
	}
}