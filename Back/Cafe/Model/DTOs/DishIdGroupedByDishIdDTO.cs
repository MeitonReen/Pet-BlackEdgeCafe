using System;

namespace Cafe.Model.DTOs
{
	public class DishIdGroupedByDishIdDTO
	{
		public Guid DishId { get; }
		public int Quantity { get; }
		public DishIdGroupedByDishIdDTO(Guid dishId, int quantity)
		{
			DishId = dishId;
			Quantity = quantity;
		}
	}
}
