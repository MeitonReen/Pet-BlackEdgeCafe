using Cafe.Databases.Identity.Contexts.Interfaces;
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Databases.Identity.Contexts.Implementations
{
	public class IdentitySqliteContext : IdentityDatabase
	{
		public IdentitySqliteContext(DbContextOptions<IdentitySqliteContext> options,
			IPasswordHasher<User> passwordHasher, string adminLogin, string adminPassword,
			string connectionString)
		: base(options, passwordHasher, adminLogin, adminPassword, connectionString)
		{
		}
		public IdentitySqliteContext(DbContextOptions<IdentitySqliteContext> options,
			IPasswordHasher<User> passwordHasher, AppSettings appSettings)
		: base(options, passwordHasher, appSettings, appSettings.Databases.Identity
			.Sqlite.ConnectionString)
		{
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlite(_connectionString);
			}
		}
	}
}
