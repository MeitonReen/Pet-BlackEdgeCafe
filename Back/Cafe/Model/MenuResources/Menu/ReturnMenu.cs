using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.MenuResources.Menu
{
	public class ReturnMenu : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		public ReturnMenu(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected async override Task ExecuteAsync(ChainRequest request)
		{
			DishShortDTO[] Dishes = await _cafeDB.Dishes
				.Select(Dish => new DishShortDTO(Dish.DishId, Dish.Name, Dish.Weight,
					Dish.Cost, null))
				.ToArrayAsync();

			request.Result = _resultGenerator.Ok(Dishes);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}