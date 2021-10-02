using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Cafe.Infrastructure.EFCoreProfiler.DI
{
	public static class ForwardedHeadersServiceCollectionExtentions
	{
		public static void ConfigureForwardedHeaders(this IServiceCollection services)
		{
			services.Configure<ForwardedHeadersOptions>(ForwardedHeadersDefault);
		}
		private static void ForwardedHeadersDefault(ForwardedHeadersOptions options)
		{
			options.ForwardedHeaders =
				ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
			options.KnownProxies.Add(IPAddress.Parse("172.17.0.3"));
			return;
		}
	}
}
