using System;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class DishesByCategory
	{
		public Guid DishId { get; set; }
		public Guid CategoryId { get; set; }

		public virtual DishCategory Category { get; set; }
		public virtual Dish Dish { get; set; }
	}
}
