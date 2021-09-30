using System;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class AppliedPromocodesInCart
	{
		public Guid PromocodeId { get; set; }
		public Guid CartId { get; set; }
		public Guid ClientId { get; set; }

		public virtual Cart C { get; set; }
		public virtual Promocode Promocode { get; set; }
	}
}
