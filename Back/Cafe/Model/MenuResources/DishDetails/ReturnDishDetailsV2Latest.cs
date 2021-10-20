using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.MenuResources.DishDetails
{
	public class ReturnDishDetailsV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Dish dish = null;
		#endregion

		public ReturnDishDetailsV2Latest(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			dish = GetParamFromChainRequest<Dish>(
				GetType().Name, request, nameof(dish));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			string[] dishCategories = await (
				from dishByCategory in _cafeDB.DishesByCategories
				where dishByCategory.DishId == dish.DishId
				join dishCategory in _cafeDB.DishCategories on
					dishByCategory.CategoryId equals dishCategory.CategoryId
				select dishCategory.Name
			).ToArrayAsync();
			
			request.Result = _resultGenerator.Ok(new DishDTO(dish, dishCategories));
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}