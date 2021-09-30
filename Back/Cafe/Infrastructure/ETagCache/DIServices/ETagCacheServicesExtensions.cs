using Cafe.Infrastructure.DIServices;
using Cafe.Infrastructure.ETagCache.Databases.Contexts.Interfaces;
using Cafe.Infrastructure.ETagCache.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Cafe.Infrastructure.ETagCache.DIServices
{
	public static class ETagCacheServicesExtensions
	{
		public static void AddETagCache<TDatabaseContext>(this IServiceCollection services)
			where TDatabaseContext : ETagCacheDatabase
		{
			services.AddDbContextScopedAsT<TDatabaseContext, ETagCacheDatabase>();

			services.AddTransient<ETagCacheService>();
		}
	}
}
