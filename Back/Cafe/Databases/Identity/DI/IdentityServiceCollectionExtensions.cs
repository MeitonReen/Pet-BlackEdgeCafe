using Cafe.Infrastructure.EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cafe.Databases.Identity.DI
{
	public static class IdentityServiceCollectionExtensions
	{
		public static void AddIdentityEFCoreScopedAsT<TUser, TDatabaseContext, ScopedAsT>(
			this IServiceCollection services, Action<IdentityOptions> options)
				where TUser : class
				where TDatabaseContext : ScopedAsT
				where ScopedAsT : DbContext
		{
			services.AddDbContextScopedAsT<TDatabaseContext, ScopedAsT>();

			services.AddIdentityCore<TUser>(options)
				.AddEntityFrameworkStores<TDatabaseContext>();
		}
	}
}
