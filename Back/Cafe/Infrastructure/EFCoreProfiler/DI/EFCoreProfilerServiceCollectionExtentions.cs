using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cafe.Infrastructure.EFCoreProfiler.DI
{
	public static class EFCoreProfilerServiceCollectionExtentions
	{
		public static void AddCafeEFCoreMiniProfiler(this IServiceCollection services, AppSettings
			appSettings)
		{
			services.AddMemoryCache();
			services
				.AddMiniProfiler(options => options.RouteBasePath = appSettings
					.Constants.EFCoreMiniProfilerBasePath)
				.AddEntityFramework();
		}
	}
}
