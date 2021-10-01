using Microsoft.AspNetCore.Builder;

namespace Cafe.Infrastructure.Logging
{
	public static class ShareUserIdForLoggingToEntirePipelineExtensions
	{
		public static IApplicationBuilder UseShareUserIdForLoggingToEntirePipeline(
		   this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ShareUserIdToEntirePipelineForLoggingMiddleware>();
		}
	}
}
