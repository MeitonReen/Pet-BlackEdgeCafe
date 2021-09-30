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
	public class ReturnDishIdsByCategories : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		public ReturnDishIdsByCategories(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			var dishIdOnCategories = await _cafeDB.DishCategories
				.Join(_cafeDB.DishesByCategories, Left => Left.CategoryId,
					Right => Right.CategoryId, (Left, Right) => new
					{
						Left.CategoryId,
						CategoryName = Left.Name,
						Right.DishId
					})
				.ToArrayAsync();

			DishIdOnCategoriesDTO[] dishIdOnCategoriesGroupedByDishId = dishIdOnCategories
				.GroupBy(Rec => Rec.DishId)
				.Select(GroupedRec => new DishIdOnCategoriesDTO(GroupedRec.Key,
					GroupedRec.Select(Rec => Rec.CategoryId).ToArray(),
					GroupedRec.Select(Rec => Rec.CategoryName).ToArray()))
				.ToArray();

			request.Result = _resultGenerator.Ok(dishIdOnCategoriesGroupedByDishId);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}