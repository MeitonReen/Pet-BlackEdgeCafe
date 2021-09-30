using System;

namespace Cafe.Model.DTOs
{
	public class DishShortOnCategoriesDTO : DishShortDTO
	{
		public Guid[] CategoryIds { get; }
		public string[] CategoryNames { get; }

		public DishShortOnCategoriesDTO(Guid dishId, string name, int weight, int cost,
			float? costIncludingPromocodes, Guid[] categoryIds, string[] categoryNames)
		: base(dishId, name, weight, cost, costIncludingPromocodes)
		{
			CategoryIds = categoryIds;
			CategoryNames = categoryNames;
		}
	}
}
