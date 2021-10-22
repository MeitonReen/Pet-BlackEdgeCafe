using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.MenuResources.DishDetails.Verificators
{
	public class IfThisDishIsExists : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _dishId = Guid.Empty;

		#region params to chain request
		private Dish dish = null;
		#endregion

		public IfThisDishIsExists(CafeDatabase cafeDB, Guid dishId)
		{
			_cafeDB = cafeDB;
			_dishId = dishId;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			dish = await _cafeDB.Dishes.SingleOrDefaultAsync(Dish => Dish.DishId == _dishId);
			if (dish == default(Dish))
			{
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Dish is not found"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
		protected override void SetParamsToChainRequest(ChainRequest request)
		{
			request.Context.Add(nameof(dish), dish);
			return;
		}
	}
}