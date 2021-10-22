using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.Dishes
{
	public class AddDish : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _dishId = Guid.Empty;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		private int dishCost = 0;
		private float? dishCostIncluding_Valid_Applied_Promocodes = null;
		#endregion

		public AddDish(CafeDatabase cafeDB, Guid dishId)
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
			PrepareToUpdateClientCart(dishCost,
				dishCostIncluding_Valid_Applied_Promocodes);
			PrepareToUpdateDishesInCarts(_dishId, dishCostIncluding_Valid_Applied_Promocodes);
			
			await _cafeDB.SaveChangesAsync();

			request.Status = ChainProcessingStatus.Success;
			return;
		}

		//Update Cart --> Amount, AmountIncluding_Valid_Applied_Promocodes
		private void PrepareToUpdateClientCart(int addingDishCost,
			float? addingDishCostIncluding_Valid_Applied_Promocodes)
		{
			clientCart.Amount += addingDishCost;

			clientCart.AmountIncludingValidAppliedPromocodes +=
				addingDishCostIncluding_Valid_Applied_Promocodes;
		}
		//Update DishInCart --> CostIncluding_Valid_Applied_Promocodes
		private void PrepareToUpdateDishesInCarts(Guid addingDishId,
			float? addingDishCostIncluding_Valid_Applied_Promocodes)
		{
			_cafeDB.DishesInCarts.Add(new DishesInCart()
			{
				CartId = clientCart.CartId,
				ClientId = clientCart.ClientId,
				DishId = addingDishId,
				CostIncludingValidAppliedPromocodes =
						addingDishCostIncluding_Valid_Applied_Promocodes
			});

			return;
		}
	}
}