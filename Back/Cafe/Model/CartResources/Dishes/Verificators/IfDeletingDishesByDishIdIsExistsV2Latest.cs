using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.Dishes.Verificators
{
	public class IfDeletingDishesByDishIdIsExistsV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _dishId = Guid.Empty;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		#endregion

		public IfDeletingDishesByDishIdIsExistsV2Latest(CafeDatabase cafeDB, Guid dishId)
		{
			_cafeDB = cafeDB;
			_dishId = dishId;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				this.GetType().Name, request, nameof(clientCart));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			DishesInCart[] DishesInClientCart = await (
				from dishInClientCart in _cafeDB.DishesInCarts
				where dishInClientCart.DishId == _dishId &&
					dishInClientCart.CartId == clientCart.CartId
				select dishInClientCart
			).ToArrayAsync();

			if (!DishesInClientCart.Any())
			{
				request.Status = ChainProcessingStatus.Failure_exit;
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Deleting dishes is not found"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}