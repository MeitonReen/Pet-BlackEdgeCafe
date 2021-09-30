
using Cafe.Databases.Cafe.Model;
using System;

namespace Cafe.Model.DTOs
{
	public class DishDTO : DishDefaultDTO
	{
		public string Description { get; }
		public int Calorie { get; }
		public string[] Categories { get; }

		public DishDTO(Guid dishId, string name, int weight, int cost, string description,
			int calorie, string[] categories)
		: base(dishId, name, weight, cost)
		{
			Description = description;
			Calorie = calorie;
			Categories = categories;
		}

		public DishDTO(Dish dish, string[] categories)
		: base(dish.DishId, dish.Name, dish.Weight, dish.Cost)
		{
			Description = dish.Description;
			Calorie = dish.Calorie;
			Categories = categories;
		}
	}

}
