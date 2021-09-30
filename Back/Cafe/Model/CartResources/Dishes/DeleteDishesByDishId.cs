using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.Dishes
{
	public class DeleteDishesByDishId : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _dishId = Guid.Empty;
		private DishesInCart[] _deletingDishesFromCart = null;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		private int dishCost = 0;
		private float? dishCostIncluding_Valid_Applied_Promocodes = null;
		#endregion

		public DeleteDishesByDishId(CafeDatabase cafeDB, Guid dishId)
		{
			_cafeDB = cafeDB;
			_dishId = dishId;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			string className = this.GetType().Name;

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
			await PrepareToUpdateClientCartAsync(dishCost, _dishId,
				dishCostIncluding_Valid_Applied_Promocodes);
			PrepareToUpdateDishesInCarts();

			await _cafeDB.SaveChangesAsync();

			request.Status = ChainProcessingStatus.Success;
			return;
		}
		//Update Cart --> Amount, AmountIncluding_Valid_Applied_Promocodes
		private async Task PrepareToUpdateClientCartAsync(int deletingDishCost, Guid deletingDishId,
			float? deletingDishCostIncluding_Valid_Applied_Promocodes)
		{
			_deletingDishesFromCart = await _cafeDB.DishesInCarts
				.Where(DishInCart => DishInCart.DishId == deletingDishId)
				.ToArrayAsync();

			int quantityDishesOfDeleteDishType = _deletingDishesFromCart.Length;

			clientCart.Amount -= (deletingDishCost * quantityDishesOfDeleteDishType);
			;
			clientCart.AmountIncludingValidAppliedPromocodes -=
				quantityDishesOfDeleteDishType *
				deletingDishCostIncluding_Valid_Applied_Promocodes;
			return;
		}
		//Update DishesInCart --> remove by deletingDishId
		private void PrepareToUpdateDishesInCarts()
		{
			_cafeDB.DishesInCarts.RemoveRange(_deletingDishesFromCart);
			return;
		}
	}
}