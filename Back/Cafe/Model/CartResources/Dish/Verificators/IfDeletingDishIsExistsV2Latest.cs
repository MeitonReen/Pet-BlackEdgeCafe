using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.Dish.Verificators
{
	public class IfDeletingDishIsExistsV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _dishId = Guid.Empty;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		#endregion

		public IfDeletingDishIsExistsV2Latest(CafeDatabase cafeDB, Guid dishId)
		{
			_cafeDB = cafeDB;
			_dishId = dishId;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				GetType().Name, request, nameof(clientCart));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			DishesInCart DishInClientCart = await (
				from dishInCart in _cafeDB.DishesInCarts
				where dishInCart.DishId == _dishId &&
					dishInCart.CartId == clientCart.CartId
				select dishInCart
			).FirstOrDefaultAsync();

			if (DishInClientCart == default(DishesInCart))
			{
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Deleting dish is not found"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}