using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.HandlersChain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.Dish
{
	public class DeleteDish : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _dishId = Guid.Empty;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		private int dishCost = 0;
		private float? dishCostIncluding_Valid_Applied_Promocodes = null;
		#endregion

		public DeleteDish(CafeDatabase cafeDB, Guid dishId)
		{
			_cafeDB = cafeDB;
			_dishId = dishId;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			string className = GetType().Name;

			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				className, request, nameof(clientCart));
			dishCost = GetParamFromChainRequest<int>(
				className, request, nameof(dishCost));
			dishCostIncluding_Valid_Applied_Promocodes = GetParamFromChainRequest<float?>(
				className, request, nameof(dishCostIncluding_Valid_Applied_Promocodes));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			PrepareToUpdateClientCart(dishCost, dishCostIncluding_Valid_Applied_Promocodes);
			await PrepareToUpdateDishesInCartsAsync(_dishId);

			await _cafeDB.SaveChangesAsync();

			request.Status = ChainProcessingStatus.Success;
			return;
		}
		//Update Cart --> Amount, AmountIncluding_Valid_Applied_Promocodes
		private void PrepareToUpdateClientCart(int deletingDishCost,
			float? deletingDishCostIncluding_Valid_Applied_Promocodes)
		{
			clientCart.Amount -= deletingDishCost;

			clientCart.AmountIncludingValidAppliedPromocodes -=
				deletingDishCostIncluding_Valid_Applied_Promocodes;
			return;
		}
		//Update DishInCart --> remove by deletingDishId
		private async Task PrepareToUpdateDishesInCartsAsync(Guid deletingDishId)
		{
			_cafeDB.DishesInCarts.Remove(await _cafeDB.DishesInCarts
				.FirstOrDefaultAsync(DishInCart => DishInCart.DishId == deletingDishId));
			return;
		}
	}
}