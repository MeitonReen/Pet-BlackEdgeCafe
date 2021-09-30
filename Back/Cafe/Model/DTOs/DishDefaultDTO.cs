using System;

namespace Cafe.Model.DTOs
{
	public class DishDefaultDTO
	{
		public Guid DishId { get; }
		public string Name { get; }
		public int Weight { get; }
		public int Cost { get; }

		public DishDefaultDTO(Guid dishId, string name, int weight, int cost)
		{
			DishId = dishId;
			Name = name;
			Weight = weight;
			Cost = cost;
		}
	}

}
