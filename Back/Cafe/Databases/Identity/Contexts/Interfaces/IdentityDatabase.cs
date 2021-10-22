using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cafe.Databases.Identity.Contexts.Interfaces
{
	public abstract class IdentityDatabase : IdentityDbContext<User, IdentityRole<Guid>, Guid>
	{
		private readonly IPasswordHasher<User> _passwordHasher = null;
		protected string _adminLogin = null;
		protected string _adminPassword = null;
		protected string _connectionString = null;
		public IdentityDatabase(DbContextOptions options, IPasswordHasher<User> passwordHasher,
			string adminLogin, string adminPassword, string connectionString)
		: base(options)
		{
			_passwordHasher = passwordHasher;
			_adminLogin = adminLogin;
			_adminPassword = adminPassword;
			_connectionString = connectionString;
		}
		public IdentityDatabase(DbContextOptions options, IPasswordHasher<User> passwordHasher,
			AppSettings appSettings, string connectionString)
		: base(options)
		{
			_passwordHasher = passwordHasher;
			_adminLogin = appSettings.ServiceAccounts.Admin.Login;
			_adminPassword = appSettings.ServiceAccounts.Admin.Password;
			_connectionString = connectionString;
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.SeedToIdentity(_passwordHasher, _adminLogin, _adminPassword);
		}
	}
}
