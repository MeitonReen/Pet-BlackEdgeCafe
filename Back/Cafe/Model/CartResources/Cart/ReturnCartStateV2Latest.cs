using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.Cart
{
	public class ReturnCartStateV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		#endregion

		public ReturnCartStateV2Latest(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				this.GetType().Name, request, nameof(clientCart));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			TableDTO Table = null;
			int Amount = clientCart.Amount;
			float? AmountIncludingPromocodes = clientCart
				.AmountIncludingValidAppliedPromocodes;
			int QuantityDishes = 0;

			DishShortGroupedByDishIdDTO[] DishesGroupedByDishId = await (
				from dishInCart in _cafeDB.DishesInCarts
				where dishInCart.CartId == clientCart.CartId
				select new DishShortDTO(dishInCart.DishId, dishInCart.Dish.Name,
					dishInCart.Dish.Weight, dishInCart.Dish.Cost,
					dishInCart.CostIncludingValidAppliedPromocodes) into dishShort
				group dishShort by dishShort into groupedDishesShort
				select new DishShortGroupedByDishIdDTO(groupedDishesShort.Key,
					groupedDishesShort.Count())
			).ToArrayAsync();

			Array.ForEach(DishesGroupedByDishId, Dish => QuantityDishes += Dish.Quantity);

			Databases.Cafe.Model.Table DBTable = await _cafeDB.Tables
				.SingleOrDefaultAsync(Table => Table.TableId == clientCart.TableId);

			if (DBTable != default(Databases.Cafe.Model.Table))
			{
				Table = new TableDTO(DBTable.TableId, DBTable.TableNumber,
					DBTable.NumberOfSeats);
			}
			request.Result = _resultGenerator.Ok(new CartStateDTO(Table, Amount,
				AmountIncludingPromocodes, QuantityDishes, DishesGroupedByDishId));
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}