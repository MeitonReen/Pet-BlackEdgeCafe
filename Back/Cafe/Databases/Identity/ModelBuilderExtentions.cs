using Cafe.Databases.Identity.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

#nullable disable

namespace Cafe.Databases.Identity
{
	public static class ModelBuilderExtentions
	{
		public static void SeedToIdentity(this ModelBuilder modelBuilder,
			IPasswordHasher<User> passwordHasher, AppSettings appSettings)
		{
			modelBuilder.Entity<User>().HasData(new User()
			{
				Id = new SequentialGuidValueGenerator().Next(null),
				UserName = appSettings.ServiceAccounts.Admin.Login,
				PasswordHash = passwordHasher.HashPassword(null,
					appSettings.ServiceAccounts.Admin.Password)
			});
		}
	}
}
