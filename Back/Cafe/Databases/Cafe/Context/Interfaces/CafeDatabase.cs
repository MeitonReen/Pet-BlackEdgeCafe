using Cafe.Databases.Cafe.Model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Cafe.Databases.Cafe.Context.Interfaces
{
	public abstract class CafeDatabase : DbContext
	{
		public CafeDatabase(DbContextOptions options)
			: base(options)
		{
		}
		public virtual DbSet<AppliedPromocodesInCart> AppliedPromocodesInCarts { get; set; }
		public virtual DbSet<AppliedPromocodesInOrder> AppliedPromocodesInOrders { get; set; }
		public virtual DbSet<BookedTable> BookedTables { get; set; }
		public virtual DbSet<Cart> Carts { get; set; }
		public virtual DbSet<CartsLinkedDish> CartsLinkedDishes { get; set; }
		public virtual DbSet<Dish> Dishes { get; set; }
		public virtual DbSet<DishCategory> DishCategories { get; set; }
		public virtual DbSet<DishesByCategory> DishesByCategories { get; set; }
		public virtual DbSet<DishesInCart> DishesInCarts { get; set; }
		public virtual DbSet<DishesInOrder> DishesInOrders { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<Promocode> Promocodes { get; set; }
		public virtual DbSet<PromocodesForDishCategory> PromocodesForDishCategories { get; set; }
		public virtual DbSet<Table> Tables { get; set; }
	}
}
