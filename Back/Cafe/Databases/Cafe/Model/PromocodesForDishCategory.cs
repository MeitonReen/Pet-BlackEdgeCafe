using System;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class PromocodesForDishCategory
	{
		public Guid PromocodeId { get; set; }
		public Guid CategoryId { get; set; }

		public virtual DishCategory Category { get; set; }
		public virtual Promocode Promocode { get; set; }
	}
}
