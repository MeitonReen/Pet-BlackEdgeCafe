using System;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class AppliedPromocodesInOrder
	{
		public Guid PromocodeId { get; set; }
		public Guid OrderId { get; set; }
		public Guid ClientId { get; set; }

		public virtual Order Order { get; set; }
		public virtual Promocode Promocode { get; set; }
	}
}
