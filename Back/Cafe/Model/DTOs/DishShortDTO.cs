using System;

namespace Cafe.Model.DTOs
{
	public class DishShortDTO : DishDefaultDTO
	{
		public float? CostIncludingPromocodes { get; }

		public DishShortDTO(Guid dishId, string name, int weight, int cost,
			float? costIncludingPromocodes)
		: base(dishId, name, weight, cost)
		{
			CostIncludingPromocodes = costIncludingPromocodes;
		}
	}

}
