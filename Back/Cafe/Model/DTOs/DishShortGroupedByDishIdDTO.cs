
using Cafe.Databases.Cafe.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cafe.Model.DTOs
{
	public class DishShortGroupedByDishIdDTO : DishShortDTO
	{
		public int Quantity { get; }

		public DishShortGroupedByDishIdDTO(Guid dishId, string name, int weight, int cost,
			float? costIncludingPromocodes, int quantity)
		: base(dishId, name, weight, cost, costIncludingPromocodes)
		{
			Quantity = quantity;
		}

		public DishShortGroupedByDishIdDTO(IGrouping<
			DishIdOnCostIncluding_Applied_PromocodesDTO, DishesInOrder>
			dishesInOrderIncludingDishDetails_GroupedByDishId)
		: base(dishesInOrderIncludingDishDetails_GroupedByDishId.Key.DishId,
			dishesInOrderIncludingDishDetails_GroupedByDishId.First().Dish.Name,
			dishesInOrderIncludingDishDetails_GroupedByDishId.First().Dish.Weight,
			dishesInOrderIncludingDishDetails_GroupedByDishId.First().Dish.Cost,
			dishesInOrderIncludingDishDetails_GroupedByDishId.Key
				.CostIncluding_Applied_PromocodesDTO)
		{
			Quantity = dishesInOrderIncludingDishDetails_GroupedByDishId.Count();
		}
		public DishShortGroupedByDishIdDTO(Guid dishId,
			float? costIncluding_Applied_PromocodesDTO, IEnumerable<DishesInOrder> dishesInOrder)
		: base(dishId,
			dishesInOrder.First().Dish.Name,
			dishesInOrder.First().Dish.Weight,
			dishesInOrder.First().Dish.Cost,
			costIncluding_Applied_PromocodesDTO)
		{
			Quantity = dishesInOrder.Count();
		}
		public DishShortGroupedByDishIdDTO(DishShortDTO dish, int quantity)
		: base(dish.DishId, dish.Name, dish.Weight, dish.Cost, dish.CostIncludingPromocodes)
		{
			Quantity = quantity;
		}
	}
}
