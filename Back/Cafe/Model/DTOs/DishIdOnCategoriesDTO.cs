using System;

namespace Cafe.Model.DTOs
{
	public class DishIdOnCategoriesDTO
	{
		public Guid DishId { get; }
		public Guid[] CategoryIds { get; }
		public string[] CategoryNames { get; }

		public DishIdOnCategoriesDTO(Guid dishId, Guid[] categoryIds, string[] categoryNames)
		{
			DishId = dishId;
			CategoryIds = categoryIds;
			CategoryNames = categoryNames;
		}
	}
}
