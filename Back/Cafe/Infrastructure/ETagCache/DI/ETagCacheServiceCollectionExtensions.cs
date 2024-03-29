﻿using Cafe.Infrastructure.EFCore.DI;
using Cafe.Infrastructure.ETagCache.Databases.Contexts.Interfaces;
using Cafe.Infrastructure.ETagCache.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Cafe.Infrastructure.ETagCache.DI
{
	public static class ETagCacheServiceCollectionExtensions
	{
		public static void AddETagCache<TDatabaseContext>(this IServiceCollection services)
			where TDatabaseContext : ETagCacheDatabase
		{
			services.AddDbContextScopedAsT<TDatabaseContext, ETagCacheDatabase>();

			services.AddTransient<ETagCacheService>();
		}
	}
}
