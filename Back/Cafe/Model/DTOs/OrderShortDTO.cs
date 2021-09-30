using System;

namespace Cafe.Model.DTOs
{
	public class OrderShortDTO
	{
		public Guid OrderId { get; }
		public int QuantityDishes { get; }
		public int Amount { get; }
		public float? AmountIncludingPromocodes { get; }
		public string CookingStatus { get; }

		public DishShortGroupedByDishIdDTO[] DishesGroupedByDishId { get; }

		public OrderShortDTO(Guid orderId, int quantityDishes, int amount,
			float? amountIncludingPromocodes, string cookingStatus,
			DishShortGroupedByDishIdDTO[] dishesGroupedByDishId)
		{
			OrderId = orderId;
			QuantityDishes = quantityDishes;
			Amount = amount;
			AmountIncludingPromocodes = amountIncludingPromocodes;
			CookingStatus = cookingStatus;
			DishesGroupedByDishId = dishesGroupedByDishId;
		}
	}
}
