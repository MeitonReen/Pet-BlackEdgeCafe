using Cafe.Infrastructure.ETagCache.Attributes.AttributeSpecParams;
using Cafe.Infrastructure.ETagCache.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.ETagCache.Attributes
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class UpdateRelatedETagsAttribute : Attribute, IAsyncResultFilter, IFilterFactory
	{
		private readonly ETagCacheService _eTagCacheService = null;
		private readonly string[] _relatedUrlGetResources = null;
		public UpdateRelatedETagsFor UpdateRelatedETagsFor { get; set; } =
			UpdateRelatedETagsFor.RequestingClient;
		public bool IsReusable { get => false; }

		public UpdateRelatedETagsAttribute(string apiVersion,
			params string[] relatedUrlGetResources)
		{
			relatedUrlGetResources = relatedUrlGetResources
				.Select(relUrl => relUrl = CorrectApiVersion(apiVersion) + relUrl)
				.ToArray();

			_relatedUrlGetResources = relatedUrlGetResources;
		}
		private static string CorrectApiVersion(string apiVersion)
		{
			return (apiVersion[0] != '/' ? '/' : "") + apiVersion +
				(apiVersion[^1] != '/' ? '/' : "");
		}
		private UpdateRelatedETagsAttribute(ETagCacheService eTagCacheService,
			string[] relatedUrlGetResources)
		{
			_eTagCacheService = eTagCacheService;
			_relatedUrlGetResources = relatedUrlGetResources;
		}

		public async Task OnResultExecutionAsync(ResultExecutingContext context,
			ResultExecutionDelegate next)
		{
			await next();//Generate result (fill httpContext.Response)

			if (IsETagResponseSupported(context.HttpContext))
			{
				await _eTagCacheService.UpdateRelatedETagsAsync(context,
					_relatedUrlGetResources, UpdateRelatedETagsFor);
			}
			return;
		}
		private static bool IsETagResponseSupported(HttpContext httpContext)
		{
			return
				httpContext.Response.StatusCode == StatusCodes.Status200OK ||
				httpContext.Response.StatusCode == StatusCodes.Status201Created ||
				httpContext.Response.StatusCode == StatusCodes.Status400BadRequest;
		}
		public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
		{
			ETagCacheService eTagCacheService = serviceProvider.GetService(typeof(
				ETagCacheService)) as ETagCacheService;

			return new UpdateRelatedETagsAttribute(eTagCacheService, _relatedUrlGetResources);
		}
	}
}
