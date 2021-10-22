using Cafe.Databases.Identity.Contexts.Interfaces;
using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Databases.Identity.Contexts.Implementations
{
	public class IdentityMssqlContext : IdentityDatabase
	{
		public IdentityMssqlContext(DbContextOptions<IdentityMssqlContext> options,
			IPasswordHasher<User> passwordHasher, string adminLogin, string adminPassword,
			string connectionString)
		: base(options, passwordHasher, adminLogin, adminPassword, connectionString)
		{
		}
		public IdentityMssqlContext(DbContextOptions<IdentityMssqlContext> options,
			IPasswordHasher<User> passwordHasher, AppSettings appSettings)
		: base(options, passwordHasher, appSettings, appSettings.Databases.Identity
			.Mssql.ConnectionString)
		{
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(_connectionString);
			}
		}
	}
}
