using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.MiniCart
{
	public class ReturnMiniCartStateV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		#endregion

		public ReturnMiniCartStateV2Latest(CafeDatabase cafeDB)
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
			DishesInCart[] DishesInCarts = await (
				from dishInCart in _cafeDB.DishesInCarts
				where dishInCart.CartId == clientCart.CartId
				select dishInCart
			).ToArrayAsync();

			int Amount = clientCart.Amount;
			float? AmountIncludingPromocodes = clientCart.AmountIncludingValidAppliedPromocodes;

			int QuantityDishes = DishesInCarts.Length;

			DishIdGroupedByDishIdDTO[] dishIdsGroupedByDishId = (
				from dish in DishesInCarts
				group dish by dish.DishId into groupedDishes
				select new DishIdGroupedByDishIdDTO(groupedDishes.Key,
					groupedDishes.Count())
			).ToArray();

			request.Result = _resultGenerator.Ok(new MiniCartStateDTO(Amount,
				AmountIncludingPromocodes, QuantityDishes, dishIdsGroupedByDishId));

			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}