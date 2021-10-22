using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.OrdersResources.Orders.Verificators
{
	public class IDishesExistsInCartV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		#endregion

		#region params to chain request
		private DishesInCart[] dishesInClientCart = null;
		#endregion

		public IDishesExistsInCartV2Latest(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				GetType().Name, request, nameof(clientCart));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			dishesInClientCart = await (
				from dishInCart in _cafeDB.DishesInCarts
				where dishInCart.CartId == clientCart.CartId
				select dishInCart
			).ToArrayAsync();

			if (!dishesInClientCart.Any())
			{
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("No dishes in client cart"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
		protected override void SetParamsToChainRequest(ChainRequest request)
		{
			request.Context.Add(nameof(dishesInClientCart), dishesInClientCart);
			return;
		}
	}
}