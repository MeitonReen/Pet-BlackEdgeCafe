using System;
using System.Collections.Generic;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class Dish
	{
		public Dish()
		{
			CartsLinkedDishes = new HashSet<CartsLinkedDish>();
			DishesByCategories = new HashSet<DishesByCategory>();
			DishesInCarts = new HashSet<DishesInCart>();
			DishesInOrders = new HashSet<DishesInOrder>();
		}

		public Guid DishId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Weight { get; set; }
		public int Cost { get; set; }
		public int Calorie { get; set; }

		public virtual ICollection<CartsLinkedDish> CartsLinkedDishes { get; set; }
		public virtual ICollection<DishesByCategory> DishesByCategories { get; set; }
		public virtual ICollection<DishesInCart> DishesInCarts { get; set; }
		public virtual ICollection<DishesInOrder> DishesInOrders { get; set; }
	}
}
