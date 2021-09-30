using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using System.Threading.Tasks;

namespace Cafe.Model.OrdersResources.Orders.Verificators
{
	public class IfTableSelectedInCart : HandlerBase
	{
		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		#endregion

		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				this.GetType().Name, request, nameof(clientCart));
			return;
		}
		protected override Task ExecuteAsync(ChainRequest request)
		{
			if (clientCart.TableId == null)
			{
				request.Status = ChainProcessingStatus.Failure_exit;
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Table in client cart is not selected"));
				return Task.CompletedTask;
			}
			request.Status = ChainProcessingStatus.Success;
			return Task.CompletedTask;
		}
	}
}