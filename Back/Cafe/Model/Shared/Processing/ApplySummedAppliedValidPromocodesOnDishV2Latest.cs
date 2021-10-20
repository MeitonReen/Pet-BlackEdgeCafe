using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.Shared.Processing
{
	public class ApplySummedAppliedValidPromocodesOnDishV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _dishId = Guid.Empty;
		private readonly PromocodeService _promocodeService = new();

		#region params from chain request
		private Cart clientCart = null;
		#endregion

		#region params to chain request
		private int dishCost = 0;
		private float? dishCostIncluding_Valid_Applied_Promocodes = null;
		#endregion

		public ApplySummedAppliedValidPromocodesOnDishV2Latest(CafeDatabase cafeDB, Guid dishId)
		{
			_cafeDB = cafeDB;
			_dishId = dishId;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			clientCart = GetParamFromChainRequest<Cart>(
				GetType().Name, request, nameof(clientCart));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			Dish Dish = await _cafeDB.Dishes
				.SingleOrDefaultAsync(Dish => Dish.DishId == _dishId);
			dishCost = Dish.Cost;

			dishCostIncluding_Valid_Applied_Promocodes = _promocodeService.ApplyPromocode(dishCost,
				await GetSummedCoefficient_Valid_Applied_PromocodesFromCartAsync(_dishId,
					clientCart));

			request.Status = ChainProcessingStatus.Success;

			;
			return;
		}
		protected override void SetParamsToChainRequest(ChainRequest request)
		{
			request.Context.Add(nameof(dishCost), dishCost);
			request.Context.Add(nameof(dishCostIncluding_Valid_Applied_Promocodes),
				dishCostIncluding_Valid_Applied_Promocodes);
			return;
		}
		private async Task<float> GetSummedCoefficient_Valid_Applied_PromocodesFromCartAsync(
			Guid dishId, Cart clientCart)
		{
			IQueryable<DishIdOnCategoryidDTO> DishIdsOnCategoryIds =
				from dishByCategory in _cafeDB.DishesByCategories
				where dishByCategory.DishId == dishId
				select new DishIdOnCategoryidDTO(dishId, dishByCategory.CategoryId);

			return await
				GetSummedCoefficient_Valid_Applied_PromocodesFromCartByOneTypeDishAsync(
					DishIdsOnCategoryIds, clientCart);
		}
		private async Task<float>
			GetSummedCoefficient_Valid_Applied_PromocodesFromCartByOneTypeDishAsync(
				IQueryable<DishIdOnCategoryidDTO> dishIdsOnCategoryIds,
				Cart clientCart)
		{
			var IsValidAppliedInCartPromocodeId_WithCoefficientDiscount_WithCategoryId = await
				GetPromocodesFromCartOnCategories(clientCart)
					.ToArrayAsync();

			var dishidsOnCategoryIdsArr = await dishIdsOnCategoryIds.ToArrayAsync();

			return _promocodeService.NormalizeCoefficientDiscount((
				from left in dishidsOnCategoryIdsArr
				join right in IsValidAppliedInCartPromocodeId_WithCoefficientDiscount_WithCategoryId on
					left.CategoryId equals right.CategoryId
				select new
				{
					left.DishId,
					right.CoefficientDiscount
				}
			).Sum(dishOnPromocodes => dishOnPromocodes.CoefficientDiscount));
		}
		private IQueryable<IsValidAppliedInCartPromocodeId_WithCoefficientDiscount_WithCategoryIdDTO>
		GetPromocodesFromCartOnCategories(Cart clientCart)
		{
			return
				from appliedPromocodeInCart in _cafeDB.AppliedPromocodesInCarts
				where appliedPromocodeInCart.CartId == clientCart.CartId
				join validPromocode in (
					from promocode in _cafeDB.Promocodes
					where promocode.IsValid
					select promocode)
				on appliedPromocodeInCart.PromocodeId equals validPromocode.PromocodeId
				select new
				{
					appliedPromocodeInCart.PromocodeId,
					validPromocode.CoefficientDiscount
				} into isValidAppliedPromocodeInCartData
				join promocodeForDishCatogory in _cafeDB.PromocodesForDishCategories on
					isValidAppliedPromocodeInCartData.PromocodeId equals
						promocodeForDishCatogory.PromocodeId
				select new IsValidAppliedInCartPromocodeId_WithCoefficientDiscount_WithCategoryIdDTO(
					isValidAppliedPromocodeInCartData.PromocodeId,
					isValidAppliedPromocodeInCartData.CoefficientDiscount,
					promocodeForDishCatogory.CategoryId);
		}
	}
}