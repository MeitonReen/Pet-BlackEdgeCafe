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
			IPasswordHasher<User> passwordHasher, AppSettings appSettings)
		: base(options, passwordHasher, appSettings)
		{
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_appSettings.Databases.Identity.Mssql.ConnectionString);
		}
	}
}
