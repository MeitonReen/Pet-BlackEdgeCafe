using System;
using System.Collections.Generic;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class Cart
	{
		public Cart()
		{
			AppliedPromocodesInCarts = new HashSet<AppliedPromocodesInCart>();
			CartsLinkedDishes = new HashSet<CartsLinkedDish>();
			DishesInCarts = new HashSet<DishesInCart>();
		}

		public Guid CartId { get; set; }
		public Guid ClientId { get; set; }
		public Guid? TableId { get; set; }
		public string CookingStatus { get; set; }
		public int Amount { get; set; }
		public float? AmountIncludingValidAppliedPromocodes { get; set; }
		public byte[] RowVersion { get; set; }
		public int SqliteVersion { get; set; }

		public virtual Table Table { get; set; }
		public virtual ICollection<AppliedPromocodesInCart> AppliedPromocodesInCarts { get; set; }
		public virtual ICollection<CartsLinkedDish> CartsLinkedDishes { get; set; }
		public virtual ICollection<DishesInCart> DishesInCarts { get; set; }
	}
}
