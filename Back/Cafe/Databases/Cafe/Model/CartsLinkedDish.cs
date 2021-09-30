using System;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class CartsLinkedDish
	{
		public Guid ClientId { get; set; }
		public Guid CartId { get; set; }
		public Guid DishId { get; set; }
		public float? CostIncludingValidAppliedPromocodes { get; set; }

		public virtual Cart C { get; set; }
		public virtual Dish Dish { get; set; }
	}
}
