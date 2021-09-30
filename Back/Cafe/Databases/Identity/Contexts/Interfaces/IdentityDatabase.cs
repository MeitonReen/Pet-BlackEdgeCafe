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
		protected AppSettings _appSettings = null;
		public IdentityDatabase(DbContextOptions options, IPasswordHasher<User> passwordHasher,
			AppSettings appSettings)
		: base(options)
		{
			_passwordHasher = passwordHasher;
			_appSettings = appSettings;
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.SeedToIdentity(_passwordHasher, _appSettings);
		}
	}
}
