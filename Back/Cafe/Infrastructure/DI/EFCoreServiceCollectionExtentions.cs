using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cafe.Infrastructure.DI
{
	public static class EFCoreServiceCollectionExtentions
	{
		public static void AddDbContextScopedAsT<TDatabase, ScopedAsT>(
			this IServiceCollection services)
				where TDatabase : ScopedAsT
				where ScopedAsT : DbContext
		{
			services.AddDbContext<TDatabase>();
			services.AddScoped<ScopedAsT, TDatabase>();
		}
	}
}
