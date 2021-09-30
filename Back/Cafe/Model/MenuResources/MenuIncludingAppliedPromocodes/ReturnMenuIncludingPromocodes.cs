using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace Cafe.Model.MenuResources.MenuIncludingAppliedPromocodes
{
	public class ReturnMenuIncludingPromocodes : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		#endregion

		public ReturnMenuIncludingPromocodes(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				GetType().Name, request, nameof(clientCart));
			return;
		}
		protected async override Task ExecuteAsync(ChainRequest request)
		{
			DishShortDTO[] dishes = await _cafeDB.Dishes
				.SelectMany(Dish => Dish.CartsLinkedDishes
					.Where(Rec => Rec.CartId == clientCart.CartId).DefaultIfEmpty(),
						(Dish, DishLinkedCart) => new DishShortDTO(Dish.DishId, Dish.Name,
							Dish.Weight, Dish.Cost,
								DishLinkedCart.CostIncludingValidAppliedPromocodes))
				.ToArrayAsync();

			request.Result = _resultGenerator.Ok(dishes);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}