namespace Cafe.Model.DTOs
{
	public class MiniCartStateDTO
	{
		public int Amount { get; }
		public float? AmountIncludingPromocodes { get; }
		public int QuantityDishes { get; }
		public DishIdGroupedByDishIdDTO[] DishIdsGroupedByDishId { get; }

		public MiniCartStateDTO(int amount, float? amountIncludingPromocodes,
			int quantityDishes, DishIdGroupedByDishIdDTO[] dishIdsGroupedByDishId)
		{
			Amount = amount;
			AmountIncludingPromocodes = amountIncludingPromocodes;
			QuantityDishes = quantityDishes;
			DishIdsGroupedByDishId = dishIdsGroupedByDishId;
		}
	}
}
