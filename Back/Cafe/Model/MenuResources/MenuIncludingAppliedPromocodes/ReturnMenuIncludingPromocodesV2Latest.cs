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
	public class ReturnMenuIncludingPromocodesV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Cart clientCart = null;
		#endregion

		public ReturnMenuIncludingPromocodesV2Latest(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			clientCart = GetParamFromChainRequest<Cart>(
				GetType().Name, request, nameof(clientCart));
			return;
		}
		protected async override Task ExecuteAsync(ChainRequest request)
		{
			DishShortDTO[] dishes = await (
				from dish in _cafeDB.Dishes
				from dishLinkedCard in dish.CartsLinkedDishes
					.Where(right => right.CartId == clientCart.CartId).DefaultIfEmpty()
				select new DishShortDTO(dish.DishId, dish.Name, dish.Weight, dish.Cost,
						dishLinkedCard.CostIncludingValidAppliedPromocodes)
			).ToArrayAsync();

			request.Result = _resultGenerator.Ok(dishes);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}