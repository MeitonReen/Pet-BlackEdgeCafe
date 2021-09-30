using System;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class DishesInOrder
	{
		public Guid DishesInOrderId { get; set; }
		public Guid OrderId { get; set; }
		public Guid ClientId { get; set; }
		public Guid DishId { get; set; }
		public float? CostIncludingAppliedPromocodes { get; set; }

		public virtual Dish Dish { get; set; }
		public virtual Order Order { get; set; }
	}
}
