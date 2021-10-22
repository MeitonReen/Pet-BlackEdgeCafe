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
			IPasswordHasher<User> passwordHasher, string adminLogin, string adminPassword)
		{
			modelBuilder.Entity<User>().HasData(new User()
			{
				Id = new SequentialGuidValueGenerator().Next(null),
				UserName = adminLogin,
				PasswordHash = passwordHasher.HashPassword(null, adminPassword)
			});
		}
	}
}
