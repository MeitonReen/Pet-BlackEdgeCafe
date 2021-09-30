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
	public class ReturnMiniCartState : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		#endregion

		public ReturnMiniCartState(CafeDatabase cafeDB)
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
			DishesInCart[] DishesInCarts = await _cafeDB.DishesInCarts
				.Where(DishInCart => DishInCart.CartId == clientCart.CartId)
				.ToArrayAsync();

			int Amount = clientCart.Amount;
			float? AmountIncludingPromocodes = clientCart.AmountIncludingValidAppliedPromocodes;

			int QuantityDishes = DishesInCarts.Length;

			DishIdGroupedByDishIdDTO[] dishIdsGroupedByDishId = DishesInCarts
				.GroupBy(Dish => Dish.DishId)
				.Select(GroupedDishes => new DishIdGroupedByDishIdDTO(GroupedDishes.Key,
					GroupedDishes.Count()))
				.ToArray();

			request.Result = _resultGenerator.Ok(new MiniCartStateDTO(Amount,
				AmountIncludingPromocodes, QuantityDishes, dishIdsGroupedByDishId));

			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}