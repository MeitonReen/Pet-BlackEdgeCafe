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
	public class ApplySummedAppliedValidPromocodesOnDish : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _dishId = Guid.Empty;
		private readonly PromocodeService _promocodeService = new();

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		#endregion

		#region params to chain request
		private int dishCost = 0;
		private float? dishCostIncluding_Valid_Applied_Promocodes = null;
		#endregion

		public ApplySummedAppliedValidPromocodesOnDish(CafeDatabase cafeDB, Guid dishId)
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
			var DishIdsOnCategoryIds = _cafeDB.DishesByCategories
				.Where(Rec => Rec.DishId == dishId)
				.Select(Category => new DishIdOnCategoryidDTO(dishId, Category.CategoryId));

			return await
				GetSummedCoefficient_Valid_Applied_PromocodesFromCartByOneTypeDishAsync(
					DishIdsOnCategoryIds, clientCart);
		}
		private async Task<float>
			GetSummedCoefficient_Valid_Applied_PromocodesFromCartByOneTypeDishAsync(
				IQueryable<DishIdOnCategoryidDTO> dishidsOnCategoryids,
				Cart clientCart)
		{
			var IsValidAppliedInCartPromocodeId_WithCoefficientDiscount_WithCategoryId =
				await GetPromocodesFromCartOnCategories(clientCart).ToArrayAsync();

			var dishidsOnCategoryidsArr = await dishidsOnCategoryids.ToArrayAsync();

			return _promocodeService.NormalizeCoefficientDiscount(dishidsOnCategoryidsArr
				.Join(IsValidAppliedInCartPromocodeId_WithCoefficientDiscount_WithCategoryId,
					Left => Left.CategoryId, Right => Right.CategoryId, (Left, Right) => new
					{
						Left.DishId,
						Right.CoefficientDiscount
					})
				.Sum(DishOnPromocodes => DishOnPromocodes.CoefficientDiscount)
			);
		}
		private IQueryable<IsValidAppliedInCartPromocodeId_WithCoefficientDiscount_WithCategoryIdDTO>
			GetPromocodesFromCartOnCategories(Cart clientCart)
		{
			return _cafeDB.AppliedPromocodesInCarts
				.Where(Promocode => Promocode.CartId == clientCart.CartId)
				.Join(_cafeDB.Promocodes.Where(Promocode => Promocode.IsValid), Left =>
					Left.PromocodeId, Right => Right.PromocodeId, (Left, Rigth) => new
					{
						Left.PromocodeId,
						Rigth.CoefficientDiscount
					})
				.Join(_cafeDB.PromocodesForDishCategories, Left => Left.PromocodeId,
					Right => Right.PromocodeId, (Left, Right) => new IsValidAppliedInCartPromocodeId_WithCoefficientDiscount_WithCategoryIdDTO(
						Left.PromocodeId,
						Left.CoefficientDiscount,
						Right.CategoryId)
				);
		}
	}
}