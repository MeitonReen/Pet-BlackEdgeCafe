using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.ETagCache.Shared;
using Cafe.Model.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.ETagCache.Attributes
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class ETagCacheAttribute : Attribute, IAsyncActionFilter, IAsyncResultFilter,
		IFilterFactory
	{
		private readonly ETagCacheService _eTagCacheService = null;
		private readonly AppSettings _appSettings = null;
		private readonly UserService _userService = null;
		public bool IsReusable { get => false; }
		public ETagCacheAttribute()
		{ }
		private ETagCacheAttribute(ETagCacheService eTagCacheService, AppSettings appSettings)
		{
			_eTagCacheService = eTagCacheService;
			_appSettings = appSettings;
			_userService = new(_appSettings);
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context,
			ActionExecutionDelegate next)
		{
			HttpContext httpContext = context.HttpContext;

			if (IsETagRequestSupported(httpContext))
			{
				CorrectClaimsIfNotAuthorize(context.ActionDescriptor.EndpointMetadata,
					httpContext);

				context.Result = await _eTagCacheService.IfETagsMatchThen304ElseNullAsync(
					httpContext);

				if (context.Result != null) return;//Next filters, action skip, but result -
												   //generate that is OnResultExecutionAsync -
												   //will be invoked
			}

			await next();//Executing action, generate result
			return;
		}
		public async Task OnResultExecutionAsync(ResultExecutingContext context,
			ResultExecutionDelegate next)
		{
			HttpContext httpContext = context.HttpContext;

			using Stream fakeResponseBody = new MemoryStream();
			SubstituteResponseBodyStream(httpContext, out Stream originalResponseBody,
				fakeResponseBody);//otherwise headers dont edit after result generate

			await next();//Generate result (fill httpContext.Response)

			if (IsETagRequestSupported(httpContext) && IsETagResponseSupported(httpContext))
			{
				string currentETag = await _eTagCacheService
					.GetETagFromDBCreateIfIsNullAsync(httpContext.User.Claims,
						httpContext.Request.Path);

				ETagCacheService.AddETagHeaders(currentETag, httpContext);
			}

			await RollbackResponseBodyToOriginalStream(httpContext, fakeResponseBody,
				originalResponseBody);
			return;
		}
		private void CorrectClaimsIfNotAuthorize(IList<object> endpointMetadata,
			HttpContext httpContext)
		{
			if (_userService.UserIdExists(httpContext.User.Claims))
			{
				return;
			}

			bool authorizeAttibuteExists = endpointMetadata
				.OfType<AuthorizeAttribute>() != null;
			bool allowAnonymousAttributeExists = endpointMetadata
				.OfType<AllowAnonymousAttribute>() != null;

			if (!authorizeAttibuteExists || authorizeAttibuteExists &&
				allowAnonymousAttributeExists)
			{
				IPAddress clientIp = httpContext.Connection.RemoteIpAddress;

				httpContext.User.AddIdentity(new ClaimsIdentity(new Claim[] {
					_userService.CreateUserIdClaim(clientIp.ToString()) }));
			}
			return;
		}
		private static async Task RollbackResponseBodyToOriginalStream(HttpContext httpContext,
			Stream fakeResponseBody, Stream originalResponseBody)
		{
			await StreamCopyTo(fakeResponseBody, originalResponseBody);
			httpContext.Response.Body = originalResponseBody;
			return;
		}
		private static void SubstituteResponseBodyStream(HttpContext httpContext,
			out Stream originalResponseBody, Stream fakeResponseBody)
		{
			originalResponseBody = httpContext.Response.Body;
			httpContext.Response.Body = fakeResponseBody;

			return;
		}
		private static async Task StreamCopyTo(Stream from, Stream to)
		{
			from.Position = 0;
			await from.CopyToAsync(to);

			return;
		}
		private static bool IsETagRequestSupported(HttpContext httpContext)
		{
			return httpContext.Request.Method == HttpMethods.Get;
		}
		private static bool IsETagResponseSupported(HttpContext httpContext)
		{
			return httpContext.Response.StatusCode == StatusCodes.Status200OK;
		}
		public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
		{
			ETagCacheService eTagCacheService = serviceProvider.GetService(typeof(
				ETagCacheService)) as ETagCacheService;
			AppSettings appSettings = serviceProvider.GetService(typeof(
				AppSettings)) as AppSettings;

			return new ETagCacheAttribute(eTagCacheService, appSettings);
		}
	}
}
