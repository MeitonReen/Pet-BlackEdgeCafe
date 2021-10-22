using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.ETagCache.Databases.Contexts.Interfaces;
using Cafe.Infrastructure.ETagCache.Databases.Model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Cafe.Infrastructure.ETagCache.Databases.Contexts.Implementations
{
	public partial class ETagCacheMssqlContext : ETagCacheDatabase
	{
		public ETagCacheMssqlContext(DbContextOptions<ETagCacheMssqlContext> options,
			AppSettings appSettings)
			: base(options, appSettings, appSettings.Databases.Cafe.Mssql
				.ConnectionString)
		{
		}
		public ETagCacheMssqlContext(DbContextOptions<ETagCacheMssqlContext> options,
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

			modelBuilder.Entity<ETag>(entity =>
			{
				entity.HasKey(e => new { e.ClientId, e.GetResourceId })
					.HasName("PK_ETags_ClientIdGetResourceId");

				entity.ToTable("ETags");

				entity.Property(e => e.GetResourceId).HasDefaultValueSql("(newsequentialid())");

				entity.Property(e => e.ETagString)
					.IsRequired()
					.HasMaxLength(44)
					.HasColumnName("ETagString");

				entity.Property(e => e.RowVersion)
					.IsRequired()
					.IsRowVersion()
					.IsConcurrencyToken();

				entity.Property(e => e.UrlGetResource)
					.IsRequired()
					.HasMaxLength(2000);
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
