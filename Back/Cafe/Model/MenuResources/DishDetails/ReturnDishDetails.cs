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
	public class ReturnDishDetails : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Dish dish = null;
		#endregion

		public ReturnDishDetails(CafeDatabase cafeDB)
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
			string[] dishCategories = await _cafeDB.DishesByCategories
				.Where(DBC => DBC.DishId == dish.DishId)
				.Join(_cafeDB.DishCategories, DBC => DBC.CategoryId, DC => DC.CategoryId,
					(DBC, DC) => DC.Name)
				.ToArrayAsync();

			request.Result = _resultGenerator.Ok(new DishDTO(dish, dishCategories));
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}