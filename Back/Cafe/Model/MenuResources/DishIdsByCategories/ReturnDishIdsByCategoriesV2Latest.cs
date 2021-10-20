using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.MenuResources.DishIdsByCategories
{
	public class ReturnDishIdsByCategoriesV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		public ReturnDishIdsByCategoriesV2Latest(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			var dishIdOnCategories = await (
				from dishCategory in _cafeDB.DishCategories
				join dishByCategory in _cafeDB.DishesByCategories on
					dishCategory.CategoryId equals dishByCategory.CategoryId
				select new
				{
					dishCategory.CategoryId,
					CategoryName = dishCategory.Name,
					dishByCategory.DishId
				}
			).ToArrayAsync();

			DishIdOnCategoriesDTO[] dishIdOnCategoriesGroupedByDishId = (
				from dishIdOnCategory in dishIdOnCategories
				group dishIdOnCategory by dishIdOnCategory.DishId into groupedRec
				select new DishIdOnCategoriesDTO(groupedRec.Key,
					groupedRec.Select(Rec => Rec.CategoryId).ToArray(),
					groupedRec.Select(Rec => Rec.CategoryName).ToArray())
			).ToArray();

			request.Result = _resultGenerator.Ok(dishIdOnCategoriesGroupedByDishId);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}