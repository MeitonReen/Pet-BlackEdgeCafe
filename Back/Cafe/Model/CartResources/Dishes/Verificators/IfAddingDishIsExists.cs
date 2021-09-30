using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.Dishes.Verificators
{
	public class IfAddingDishIsExists : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _dishId = Guid.Empty;

		public IfAddingDishIsExists(CafeDatabase cafeDB, Guid dishId)
		{
			_cafeDB = cafeDB;
			_dishId = dishId;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			Databases.Cafe.Model.Dish AddedDish = await _cafeDB.Dishes.SingleOrDefaultAsync(Dish => Dish.DishId == _dishId);

			if (AddedDish == default(Databases.Cafe.Model.Dish))
			{
				request.Status = ChainProcessingStatus.Failure_exit;
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Adding dish is not found"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}