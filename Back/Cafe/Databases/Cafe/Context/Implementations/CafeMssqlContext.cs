using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Cafe.Databases.Cafe.Context.Implementations
{
	public partial class CafeMssqlContext : CafeDatabase
	{
		public CafeMssqlContext(DbContextOptions<CafeMssqlContext> options,
			AppSettings appSettings)
			: base(options, appSettings, appSettings.Databases.Cafe.Mssql
				.ConnectionString)
		{
		}
		public CafeMssqlContext(DbContextOptions<CafeMssqlContext> options,
			string adminLogin, string adminPassword, string connectionString)
			: base(options, adminLogin, adminPassword, connectionString)
		{
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(_connectionString);
			}
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

			modelBuilder.Entity<AppliedPromocodesInCart>(entity =>
			{
				entity.HasKey(e => new { e.PromocodeId, e.CartId })
					.HasName("PK_AppliedPromocodesInCarts_PromocodeIdCartId");

				entity.HasOne(d => d.Promocode)
					.WithMany(p => p.AppliedPromocodesInCarts)
					.HasForeignKey(d => d.PromocodeId)
					.HasConstraintName("FK_AppliedPromocodesInCarts_PromocodeId");

				entity.HasOne(d => d.C)
					.WithMany(p => p.AppliedPromocodesInCarts)
					.HasForeignKey(d => new { d.CartId, d.ClientId })
					.HasConstraintName("FK_AppliedPromocodesInCarts_OrdersIdClientId");
			});

			modelBuilder.Entity<AppliedPromocodesInOrder>(entity =>
			{
				entity.HasKey(e => new { e.PromocodeId, e.OrderId })
					.HasName("PK_AppliedPromocodesInOrders_PromocodeIdOrderId");

				entity.HasOne(d => d.Promocode)
					.WithMany(p => p.AppliedPromocodesInOrders)
					.HasForeignKey(d => d.PromocodeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_AppliedPromocodesInOrders_PromocodeId");

				entity.HasOne(d => d.Order)
					.WithMany(p => p.AppliedPromocodesInOrders)
					.HasForeignKey(d => new { d.OrderId, d.ClientId })
					.HasConstraintName("FK_AppliedPromocodesInOrders_OrdersIdClientId");
			});

			modelBuilder.Entity<BookedTable>(entity =>
			{
				entity.HasKey(e => e.BookedTableId)
					.HasName("PK_BookedTables_BookedTableId");

				entity.HasOne(d => d.Table)
					.WithMany(p => p.BookedTables)
					.HasForeignKey(d => d.TableId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_BookedTables_TableId");
			});

			modelBuilder.Entity<Cart>(entity =>
			{
				entity.HasKey(e => new { e.CartId, e.ClientId })
					.HasName("PK_Carts_CartIdClientId");

				entity.Property(e => e.CartId).HasDefaultValueSql("(newsequentialid())");

				entity.Property(e => e.AmountIncludingValidAppliedPromocodes).HasColumnName("AmountIncluding_Valid_Applied_Promocodes");

				entity.Property(e => e.CookingStatus).HasMaxLength(2000);

				entity.Property(e => e.RowVersion)
					.IsRequired()
					.IsRowVersion()
					.IsConcurrencyToken();

				entity.HasOne(d => d.Table)
					.WithMany(p => p.Carts)
					.HasForeignKey(d => d.TableId)
					.HasConstraintName("FK_Carts_TableId");
			});

			modelBuilder.Entity<CartsLinkedDish>(entity =>
			{
				entity.HasKey(e => new { e.ClientId, e.CartId, e.DishId })
					.HasName("PK_CartsLinkedDishes_CartIdClientIdDishId");

				entity.Property(e => e.CostIncludingValidAppliedPromocodes).HasColumnName("CostIncluding_Valid_Applied_Promocodes");

				entity.HasOne(d => d.Dish)
					.WithMany(p => p.CartsLinkedDishes)
					.HasForeignKey(d => d.DishId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_CartsLinkedDishes_DishId");

				entity.HasOne(d => d.C)
					.WithMany(p => p.CartsLinkedDishes)
					.HasForeignKey(d => new { d.CartId, d.ClientId })
					.HasConstraintName("FK_CartsLinkedDishes_CartIdClientId");
			});

			modelBuilder.Entity<Dish>(entity =>
			{
				entity.HasKey(e => e.DishId)
					.HasName("PK_Dishes_DishId");

				entity.HasIndex(e => e.Name, "AK_Dishes_Name")
					.IsUnique();

				entity.Property(e => e.DishId).HasDefaultValueSql("(newsequentialid())");

				entity.Property(e => e.Description).HasMaxLength(2000);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(250);
			});

			modelBuilder.Entity<DishCategory>(entity =>
			{
				entity.HasKey(e => e.CategoryId)
					.HasName("PK_DishCategories_CategoryId");

				entity.HasIndex(e => e.Name, "AK_DishCategories_Name")
					.IsUnique();

				entity.Property(e => e.CategoryId).HasDefaultValueSql("(newsequentialid())");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(256);
			});

			modelBuilder.Entity<DishesByCategory>(entity =>
			{
				entity.HasKey(e => new { e.DishId, e.CategoryId })
					.HasName("PK_DishesByCaregories_DishIdCategoryId");

				entity.HasOne(d => d.Category)
					.WithMany(p => p.DishesByCategories)
					.HasForeignKey(d => d.CategoryId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_DishesByCategories_CategoryId");

				entity.HasOne(d => d.Dish)
					.WithMany(p => p.DishesByCategories)
					.HasForeignKey(d => d.DishId)
					.HasConstraintName("FK_DishesByCategories_DishId");
			});

			modelBuilder.Entity<DishesInCart>(entity =>
			{
				entity.HasKey(e => e.DishesInCartsId)
					.HasName("PK_DishesInCarts_DishesInCartsId");

				entity.Property(e => e.DishesInCartsId).HasDefaultValueSql("(newsequentialid())");

				entity.Property(e => e.CostIncludingValidAppliedPromocodes).HasColumnName("CostIncluding_Valid_Applied_Promocodes");

				entity.HasOne(d => d.Dish)
					.WithMany(p => p.DishesInCarts)
					.HasForeignKey(d => d.DishId)
					.HasConstraintName("FK_DishesInCarts_DishId");

				entity.HasOne(d => d.C)
					.WithMany(p => p.DishesInCarts)
					.HasForeignKey(d => new { d.CartId, d.ClientId })
					.HasConstraintName("FK_DishesInCarts_CartIdClientId");
			});

			modelBuilder.Entity<DishesInOrder>(entity =>
			{
				entity.HasKey(e => e.DishesInOrderId)
					.HasName("PK_DishesInOrders_DishesInOrderId");

				entity.Property(e => e.DishesInOrderId).HasDefaultValueSql("(newsequentialid())");

				entity.Property(e => e.CostIncludingAppliedPromocodes).HasColumnName("CostIncluding_Applied_Promocodes");

				entity.HasOne(d => d.Dish)
					.WithMany(p => p.DishesInOrders)
					.HasForeignKey(d => d.DishId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_DishesInOrders_DishId");

				entity.HasOne(d => d.Order)
					.WithMany(p => p.DishesInOrders)
					.HasForeignKey(d => new { d.OrderId, d.ClientId })
					.HasConstraintName("FK_DishesInOrders_OrderIdClientId");
			});

			modelBuilder.Entity<Order>(entity =>
			{
				entity.HasKey(e => new { e.OrderId, e.ClientId })
					.HasName("PK_Orders_OrderIdClientId");

				entity.Property(e => e.OrderId).HasDefaultValueSql("(newsequentialid())");

				entity.Property(e => e.AmountIncludingAppliedPromocodes).HasColumnName("AmountIncluding_Applied_Promocodes");

				entity.Property(e => e.CookingStatus).HasMaxLength(2000);

				entity.HasOne(d => d.Table)
					.WithMany(p => p.Orders)
					.HasForeignKey(d => d.TableId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Orders_TableId");
			});

			modelBuilder.Entity<Promocode>(entity =>
			{
				entity.HasKey(e => e.PromocodeId)
					.HasName("PK_Promocodes_PromocodeId");

				entity.HasIndex(e => e.PromocodeText, "AK_Promocodes_PromocodeText")
					.IsUnique();

				entity.Property(e => e.PromocodeId).HasDefaultValueSql("(newsequentialid())");

				entity.Property(e => e.IsValid)
					.IsRequired()
					.HasDefaultValueSql("((1))");

				entity.Property(e => e.PromocodeText)
					.IsRequired()
					.HasMaxLength(100);
			});

			modelBuilder.Entity<PromocodesForDishCategory>(entity =>
			{
				entity.HasKey(e => new { e.PromocodeId, e.CategoryId })
					.HasName("PK_PromocodesForDishCategories_PromocodeIdDishCategoryId");

				entity.HasOne(d => d.Category)
					.WithMany(p => p.PromocodesForDishCategories)
					.HasForeignKey(d => d.CategoryId)
					.HasConstraintName("FK_PromocodesForDishCategories_CategoryId");

				entity.HasOne(d => d.Promocode)
					.WithMany(p => p.PromocodesForDishCategories)
					.HasForeignKey(d => d.PromocodeId)
					.HasConstraintName("FK_PromocodesForDishCategories_PromocodeId");
			});

			modelBuilder.Entity<Table>(entity =>
			{
				entity.HasKey(e => e.TableId)
					.HasName("PK_Tables_TableId");
				entity.HasIndex(e => e.TableNumber, "AK_Tables_TableNumber")
					.IsUnique();

				entity.Property(e => e.TableId).HasDefaultValueSql("(newsequentialid())");
			});

			OnModelCreatingPartial(modelBuilder);

			modelBuilder.SeedToCafeDB();
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
