using System;
using System.Collections.Generic;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class DishCategory
	{
		public DishCategory()
		{
			DishesByCategories = new HashSet<DishesByCategory>();
			PromocodesForDishCategories = new HashSet<PromocodesForDishCategory>();
		}

		public Guid CategoryId { get; set; }
		public string Name { get; set; }

		public virtual ICollection<DishesByCategory> DishesByCategories { get; set; }
		public virtual ICollection<PromocodesForDishCategory> PromocodesForDishCategories { get; set; }
	}
}
