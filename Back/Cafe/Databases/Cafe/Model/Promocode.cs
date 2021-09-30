using System;
using System.Collections.Generic;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class Promocode
	{
		public Promocode()
		{
			AppliedPromocodesInCarts = new HashSet<AppliedPromocodesInCart>();
			AppliedPromocodesInOrders = new HashSet<AppliedPromocodesInOrder>();
			PromocodesForDishCategories = new HashSet<PromocodesForDishCategory>();
		}

		public Guid PromocodeId { get; set; }
		public string PromocodeText { get; set; }
		public float CoefficientDiscount { get; set; }
		public bool IsValid { get; set; }

		public virtual ICollection<AppliedPromocodesInCart> AppliedPromocodesInCarts { get; set; }
		public virtual ICollection<AppliedPromocodesInOrder> AppliedPromocodesInOrders { get; set; }
		public virtual ICollection<PromocodesForDishCategory> PromocodesForDishCategories { get; set; }
	}
}
