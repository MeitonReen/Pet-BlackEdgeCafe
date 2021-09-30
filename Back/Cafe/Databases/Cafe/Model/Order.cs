using System;
using System.Collections.Generic;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class Order
	{
		public Order()
		{
			AppliedPromocodesInOrders = new HashSet<AppliedPromocodesInOrder>();
			DishesInOrders = new HashSet<DishesInOrder>();
		}

		public Guid OrderId { get; set; }
		public Guid ClientId { get; set; }
		public Guid TableId { get; set; }
		public DateTime DateTimeIsCompleted { get; set; }
		public string CookingStatus { get; set; }
		public int Amount { get; set; }
		public float? AmountIncludingAppliedPromocodes { get; set; }

		public virtual Table Table { get; set; }
		public virtual ICollection<AppliedPromocodesInOrder> AppliedPromocodesInOrders { get; set; }
		public virtual ICollection<DishesInOrder> DishesInOrders { get; set; }
	}
}
