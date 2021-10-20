using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.MenuResources.Menu
{
	public class ReturnMenuV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		public ReturnMenuV2Latest(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected async override Task ExecuteAsync(ChainRequest request)
		{
			DishShortDTO[] Dishes = await (
				from dish in _cafeDB.Dishes
				select new DishShortDTO(dish.DishId, dish.Name, dish.Weight, dish.Cost, null)
			).ToArrayAsync();
			
			request.Result = _resultGenerator.Ok(Dishes);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}