namespace Cafe.Model.DTOs
{
	public class CartStateDTO
	{
		public TableDTO Table { get; }
		public int Amount { get; }
		public float? AmountIncludingPromocodes { get; }
		public int QuantityDishes { get; }
		public DishShortGroupedByDishIdDTO[] DishesGroupedByDishId { get; }

		public CartStateDTO(TableDTO table, int amount, float? amountIncludingPromocodes,
			int quantityDishes, DishShortGroupedByDishIdDTO[] dishesGroupedByDishId)
		{
			Table = table;
			Amount = amount;
			AmountIncludingPromocodes = amountIncludingPromocodes;
			QuantityDishes = quantityDishes;
			DishesGroupedByDishId = dishesGroupedByDishId;
		}
	}
}
