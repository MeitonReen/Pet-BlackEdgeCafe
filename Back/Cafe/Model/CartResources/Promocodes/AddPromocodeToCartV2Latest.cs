using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Cafe.Model.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.Promocodes
{
	public class AddPromocodeToCartV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private DishesInCart[] _dishesInCart = null;
		private readonly PromocodeService _promocodeService = new();

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		private Promocode addingPromocode = null;
		#endregion

		public AddPromocodeToCartV2Latest(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			string className = GetType().Name;

			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				className, request, nameof(clientCart));
			addingPromocode = GetParamFromChainRequest<Promocode>(
				className, request, nameof(addingPromocode));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			_cafeDB.AppliedPromocodesInCarts.Add(new AppliedPromocodesInCart()
			{
				CartId = clientCart.CartId,
				ClientId = clientCart.ClientId,
				PromocodeId = addingPromocode.PromocodeId
			});
			await _cafeDB.SaveChangesAsync();

			IQueryable<Databases.Cafe.Model.Dish> dishIdsFromMenu_WithCost =
				from dish in _cafeDB.Dishes
				select dish;

			DishIdOnCostIncluding_Valid_Applied_PromocodesDTO[]
				dishIdsFromMenuOn_CostIncludinPromocodeInCart =
					await GetDishesIncludingPromocodesFromCartAsync(_cafeDB, clientCart,
						dishIdsFromMenu_WithCost);
			;
			await PrepareToUpdateDishesLinkedCartAsync(
				dishIdsFromMenuOn_CostIncludinPromocodeInCart);
			await PrepareToUpdateDishesInCartAsync(
				dishIdsFromMenuOn_CostIncludinPromocodeInCart);
			PrepareToUpdateCart();

			await _cafeDB.SaveChangesAsync();

			request.Status = ChainProcessingStatus.Success;
			return;
		}
		//Update: if empty to create; CostIncluding_Valid_Applied_Promocodes
		private async Task PrepareToUpdateDishesLinkedCartAsync(
			DishIdOnCostIncluding_Valid_Applied_PromocodesDTO[]
				dishIdsFromMenuOn_CostIncludinPromocodeInCart)
		{
			CartsLinkedDish[] dishes = await (
				from dish in _cafeDB.CartsLinkedDishes
				where dish.CartId == clientCart.CartId
				select dish
			).ToArrayAsync();

			if (!dishes.Any())
			{
				_cafeDB.CartsLinkedDishes.AddRange(await (
					from dish in _cafeDB.Dishes
					select new CartsLinkedDish()
					{
						ClientId = clientCart.ClientId,
						CartId = clientCart.CartId,
						DishId = dish.DishId
					}).ToArrayAsync()
				);
				await _cafeDB.SaveChangesAsync();
			}

			CartsLinkedDish[] dishesLinkedCart = await (
				from dish in _cafeDB.CartsLinkedDishes
				where dish.CartId == clientCart.CartId
				select dish
			).ToArrayAsync();

			dishesLinkedCart = (
				from left in dishesLinkedCart
				join right in dishIdsFromMenuOn_CostIncludinPromocodeInCart on
					left.DishId equals right.DishId
				select new Func<CartsLinkedDish, DishIdOnCostIncluding_Valid_Applied_PromocodesDTO,
					CartsLinkedDish>((left, right) =>
					{
						left.CostIncludingValidAppliedPromocodes = right
							.CostIncluding_Valid_Applied_Promocodes;
						return left;
					})(left, right)
			).ToArray();
		}
		//Update: CostIncluding_Valid_Applied_Promocodes
		private async Task PrepareToUpdateDishesInCartAsync(
			DishIdOnCostIncluding_Valid_Applied_PromocodesDTO[]
			dishIdsFromMenuOn_CostIncludinPromocodeInCart)
		{
			_dishesInCart = await (
				from dishInCart in _cafeDB.DishesInCarts
				where dishInCart.CartId == clientCart.CartId
				select dishInCart
			).ToArrayAsync();

			_dishesInCart = (
				from left in _dishesInCart
				join right in dishIdsFromMenuOn_CostIncludinPromocodeInCart on
					left.DishId equals right.DishId
				select new Func<DishesInCart, DishIdOnCostIncluding_Valid_Applied_PromocodesDTO,
					DishesInCart>((left, right) =>
					{
						left.CostIncludingValidAppliedPromocodes =
							right.CostIncluding_Valid_Applied_Promocodes;
						return left;
					})(left, right)
			).ToArray();

			return;
		}
		//Update: ClientCart --> AmountIncluding_Valid_Applied_Promocodes
		private void PrepareToUpdateCart()
		{
			clientCart.AmountIncludingValidAppliedPromocodes = _dishesInCart
				.Sum(Dishes => Dishes.CostIncludingValidAppliedPromocodes);
			return;
		}
		private async Task<DishIdOnCostIncluding_Valid_Applied_PromocodesDTO[]>
			GetDishesIncludingPromocodesFromCartAsync(CafeDatabase cafeDB,
				Databases.Cafe.Model.Cart clientCart,
				IQueryable<Databases.Cafe.Model.Dish> dishes)
		{
			var dishIdsInCart_WithCost_WithCategoryId = await (
				from dish in dishes
				join category in cafeDB.DishesByCategories on
					dish.DishId equals category.DishId
				select new
				{
					dish.DishId,
					dish.Cost,
					category.CategoryId
				}
			).ToArrayAsync();

			IQueryable<AppliedPromocodesInCart> appliedPromocodesIdsInCart =
				from promocode in cafeDB.AppliedPromocodesInCarts
				where promocode.CartId == clientCart.CartId
				select promocode;

			var appliedPromocodesidsInCart__WithCoefficientDiscount_OnlyIsValid =
				from appliedPromocodeIdInCart in appliedPromocodesIdsInCart
				join validPromocode in (
					from promocode in cafeDB.Promocodes
					where promocode.IsValid
					select promocode)
				on appliedPromocodeIdInCart.PromocodeId equals validPromocode.PromocodeId
				select new
				{
					appliedPromocodeIdInCart.PromocodeId,
					validPromocode.CoefficientDiscount
				};

			var appliedPromocodesidsInCart_WithCoefficientDiscount_OnlyIsValid_WithCategoryId =
				await (
					from left in appliedPromocodesidsInCart__WithCoefficientDiscount_OnlyIsValid
					join right in cafeDB.PromocodesForDishCategories on
						left.PromocodeId equals right.PromocodeId
					select new
					{
						left.PromocodeId,
						left.CoefficientDiscount,
						right.CategoryId
					}
				).ToArrayAsync();

			var dishIds_WithCost_On_CoefficientsDiscount_InCart_GroupedByDishId =
				from left in dishIdsInCart_WithCost_WithCategoryId
				from right in appliedPromocodesidsInCart_WithCoefficientDiscount_OnlyIsValid_WithCategoryId
					.Where(right => right.CategoryId == left.CategoryId)
					.DefaultIfEmpty()
				select new
				{
					left.DishId,
					left.Cost,
					CoefficientDiscount = right?.CoefficientDiscount ?? 0
				} into leftJoinResult
				group leftJoinResult by new
				{
					leftJoinResult.DishId,
					leftJoinResult.Cost
				};


			var dishIds_WithCost_On_SummedCoefficientsDiscount_InCart = (
				from groupedRec in dishIds_WithCost_On_CoefficientsDiscount_InCart_GroupedByDishId
				select new
				{
					groupedRec.Key.DishId,
					groupedRec.Key.Cost,
					SummedCoefficientDiscount = _promocodeService.NormalizeCoefficientDiscount(
						groupedRec.Sum(Rec => Rec.CoefficientDiscount))
				})
			.ToArray();

			IEnumerable<DishIdOnCostIncluding_Valid_Applied_PromocodesDTO>
				dishIdsOn_CostIncludinPromocodeInCart =
					from rec in dishIds_WithCost_On_SummedCoefficientsDiscount_InCart
					select new DishIdOnCostIncluding_Valid_Applied_PromocodesDTO(
						rec.DishId, _promocodeService.ApplyPromocode(
							rec.Cost, rec.SummedCoefficientDiscount));

			return dishIdsOn_CostIncludinPromocodeInCart.ToArray();
		}
	}
}