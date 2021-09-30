using System;

namespace Cafe.Model.DTOs
{
	public class DishIdOnCategoryidDTO
	{
		public Guid DishId { get; }
		public Guid CategoryId { get; }

		public DishIdOnCategoryidDTO(Guid dishId, Guid categoryId)
		{
			DishId = dishId;
			CategoryId = categoryId;
		}
	}
}
