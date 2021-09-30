using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Cafe.Model.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.Promocodes
{
	public class AddPromocodeToCart : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private DishesInCart[] _dishesInCart = null;
		private readonly PromocodeService _promocodeService = new();

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		private Promocode addingPromocode = null;
		#endregion

		public AddPromocodeToCart(CafeDatabase cafeDB)
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

			var dishIdsFromMenu_WithCost = _cafeDB.Dishes
				.Select(Dish => Dish);

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
			CartsLinkedDish[] dishes = await _cafeDB.CartsLinkedDishes
				.Where(Dish => Dish.CartId == clientCart.CartId)
				.ToArrayAsync();
			if (!dishes.Any())
			{
				_cafeDB.CartsLinkedDishes.AddRange(_cafeDB.Dishes
					.Select(Dish => new CartsLinkedDish()
					{
						ClientId = clientCart.ClientId,
						CartId = clientCart.CartId,
						DishId = Dish.DishId
					}));
				await _cafeDB.SaveChangesAsync();
			}
			var dishesLinkedCart = await _cafeDB.CartsLinkedDishes
				.Where(Dish => Dish.CartId == clientCart.CartId)
				.ToArrayAsync();

			dishesLinkedCart = dishesLinkedCart
				.Join(dishIdsFromMenuOn_CostIncludinPromocodeInCart, Left => Left.DishId,
					Right => Right.DishId,
					(Left, Right) =>
					{
						Left.CostIncludingValidAppliedPromocodes = Right
							.CostIncluding_Valid_Applied_Promocodes;
						return Left;
					})
				.ToArray();
		}
		//Update: CostIncluding_Valid_Applied_Promocodes
		private async Task PrepareToUpdateDishesInCartAsync(
			DishIdOnCostIncluding_Valid_Applied_PromocodesDTO[]
			dishIdsFromMenuOn_CostIncludinPromocodeInCart)
		{
			_dishesInCart = await _cafeDB.DishesInCarts
				.Where(DishInCart => DishInCart.CartId == clientCart.CartId)
				.ToArrayAsync();

			_dishesInCart = _dishesInCart
				.Join(dishIdsFromMenuOn_CostIncludinPromocodeInCart,
					Left => Left.DishId,
					Right => Right.DishId, (Left, Right) =>
					{
						Left.CostIncludingValidAppliedPromocodes =
							Right.CostIncluding_Valid_Applied_Promocodes;
						return Left;
					})
				.ToArray();
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
			var dishIdsInCart_WithCost_WithCategoryId = await dishes
				.Join(cafeDB.DishesByCategories, Dish => Dish.DishId, Category =>
					Category.DishId, (Dish, Category) => new
					{
						Dish.DishId,
						Dish.Cost,
						Category.CategoryId
					})
				.ToArrayAsync();
			;
			var appliedPromocodesidsInCart = cafeDB.AppliedPromocodesInCarts
				.Where(Promocode => Promocode.CartId == clientCart.CartId);

			var appliedPromocodesidsInCart__WithCoefficientDiscount_OnlyIsValid =
				appliedPromocodesidsInCart
					.Join(cafeDB.Promocodes.Where(Promocode => Promocode.IsValid),
						Left => Left.PromocodeId, Right => Right.PromocodeId,
						(Left, Right) => new
						{
							Left.PromocodeId,
							Right.CoefficientDiscount
						});
			;
			var appliedPromocodesidsInCart__WithCoefficientDiscount_OnlyIsValid_WithCategoryId =
				await appliedPromocodesidsInCart__WithCoefficientDiscount_OnlyIsValid
					.Join(cafeDB.PromocodesForDishCategories, Left => Left.PromocodeId,
						Right => Right.PromocodeId, (Left, Right) => new
						{
							Left.PromocodeId,
							Left.CoefficientDiscount,
							Right.CategoryId
						})
					.ToArrayAsync();
			;
			var dishIds_WithCost_On_CoefficientsDiscount_InCart_GroupedByDishId =
				dishIdsInCart_WithCost_WithCategoryId
					.GroupJoin(appliedPromocodesidsInCart__WithCoefficientDiscount_OnlyIsValid_WithCategoryId,
						Left => Left.CategoryId, Right => Right.CategoryId, (Left, GRight) =>
							new { Left, GRight })
					.SelectMany(RightsOnLeft => RightsOnLeft.GRight.DefaultIfEmpty(),
						(RightsOnLeft, Right) => new
						{
							RightsOnLeft.Left.DishId,
							RightsOnLeft.Left.Cost,
							CoefficientDiscount = Right?.CoefficientDiscount ?? 0
						})
						.GroupBy(GroupBy => new
						{
							GroupBy.DishId,
							GroupBy.Cost
						});
			;
			var dishIds_WithCost_On_SummedCoefficientsDiscount_InCart =
				dishIds_WithCost_On_CoefficientsDiscount_InCart_GroupedByDishId
					.Select(GroupedRec => new
					{
						GroupedRec.Key.DishId,
						GroupedRec.Key.Cost,
						SummedCoefficientDiscount = _promocodeService.NormalizeCoefficientDiscount(
							GroupedRec.Sum(Rec => Rec.CoefficientDiscount))
					})
					.ToArray();

			var dishIdsOn_CostIncludinPromocodeInCart =
				dishIds_WithCost_On_SummedCoefficientsDiscount_InCart
					.Select(Rec => new DishIdOnCostIncluding_Valid_Applied_PromocodesDTO(
						Rec.DishId, _promocodeService.ApplyPromocode(Rec.Cost,
							Rec.SummedCoefficientDiscount)));

			return dishIdsOn_CostIncludinPromocodeInCart.ToArray();
		}
	}
}