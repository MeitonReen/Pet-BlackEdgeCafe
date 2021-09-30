using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.ETagCache.Attributes.AttributeSpecParams;
using Cafe.Infrastructure.ETagCache.Databases.Contexts.Interfaces;
using Cafe.Infrastructure.ETagCache.Databases.Model;
using Cafe.Model.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.ETagCache.Shared
{
	public class ETagCacheService
	{
		private readonly AppSettings _appSettings = null;
		private readonly ETagCacheDatabase _eTagCacheContext = null;
		private readonly UserService _userService = null;
		public ETagCacheService(AppSettings appSettings, ETagCacheDatabase eTagCacheContext)
		{
			_eTagCacheContext = eTagCacheContext;
			_appSettings = appSettings;
			_userService = new UserService(_appSettings);
		}
		public async Task UpdateDBAsync()
		{
			await _eTagCacheContext.SaveChangesAsync();
		}
		public async Task<IActionResult> IfETagsMatchThen304ElseNullAsync(HttpContext
			httpContext)
		{
			string eTagFromDB = await GetETagFromDBAsync(httpContext);
			;
			if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfNoneMatch,
				 out var requestETag) && eTagFromDB == requestETag && eTagFromDB != null)
			{
				AddETagHeaders(requestETag, httpContext);

				return new StatusCodeResult(StatusCodes.Status304NotModified);
			}
			return null;
		}
		public async Task<string> GetETagFromDBAsync(HttpContext httpContext)
		{
			Guid userId = GetUserId(httpContext.User.Claims);
			string urlGetResource = httpContext.Request.Path;

			return (await GetETagTupleFromDBAsync(userId, urlGetResource))?.ETagString;
		}
		public async Task<ETag[]> GetETagTuplesFromDBAsync(string urlGetResource)
		{
			return await _eTagCacheContext.ETags.Where(eTag =>
				eTag.UrlGetResource == urlGetResource)
				.ToArrayAsync();
		}
		public async Task<ETag> GetETagTupleFromDBAsync(Guid userId, string urlGetResource)
		{
			return await _eTagCacheContext.ETags.SingleOrDefaultAsync(eTag =>
				eTag.ClientId == userId &&
				eTag.UrlGetResource == urlGetResource);
		}
		public static void AddCacheControlHeaderForETag(HttpContext httpContext)
		{
			httpContext.Response.Headers.Add(HeaderNames.CacheControl, new StringValues(
				new string[]
				{
					CacheControlHeaderValue.NoCacheString,
					CacheControlHeaderValue.PrivateString
				}));
		}
		public static void AddETagHeaders(string eTag, HttpContext httpContext)
		{
			AddETagHeader(eTag, httpContext);
			AddCacheControlHeaderForETag(httpContext);
		}
		public static void AddETagHeader(string eTag, HttpContext httpContext)
		{
			httpContext.Response.Headers.Add(HeaderNames.ETag, new StringValues(eTag));
		}
		private static async Task<KeyValuePair<string, string>> GetParameter(
			ParameterDescriptor parameter, ControllerBase controller)
		{
			var returnParameter = Activator.CreateInstance(parameter?.ParameterType);
			await controller.TryUpdateModelAsync(parameter,
				parameter.ParameterType, string.Empty);

			return new KeyValuePair<string, string>(parameter.Name,
				returnParameter.ToString());
		}
		private static void SetParameterToUrl(KeyValuePair<string, string> parameter,
			ref string url)
		{
			url = url.Replace("{" + parameter.Key + "}", parameter.Value);
			return;
		}
		public async Task UpdateRelatedETagsAsync(ResultExecutingContext context,
			string[] relatedUrlGetResources, UpdateRelatedETagsFor updateRelatedETagsFor)
		{
			foreach (string urlGetResource in relatedUrlGetResources)
			{
				if (urlGetResource.Contains('{'))
				{
					FillCurrentRequestParamsToUrlGetResource(
						context.Controller as ControllerBase, urlGetResource,
						context.ActionDescriptor.Parameters);
				}

				if (updateRelatedETagsFor == UpdateRelatedETagsFor.RequestingClient)
				{
					Guid userId = GetUserId(context.HttpContext.User.Claims);
					await PrepareToUpdateETagForClientAsync(userId, urlGetResource);
				}
				else if (updateRelatedETagsFor == UpdateRelatedETagsFor.AllClient)
				{
					await PrepareToUpdateETagForAllClientAsync(urlGetResource);
				}
			}

			await UpdateDBAsync();

			return;
		}
		public string FillCurrentRequestParamsToUrlGetResource(ControllerBase controller,
			string urlGetResource, IList<ParameterDescriptor> parameters)
		{
			parameters.ToList().ForEach(async parameter =>
				SetParameterToUrl(await GetParameter(parameter, controller),
					ref urlGetResource));

			return urlGetResource;
		}
		public async Task PrepareToUpdateETagWithParamsAsync(ControllerBase controller,
			Guid userId, string urlGetResource, IList<ParameterDescriptor> parameters)
		{
			parameters.ToList().ForEach(async parameter =>
				SetParameterToUrl(await GetParameter(parameter, controller),
					ref urlGetResource));

			await PrepareToUpdateETagForClientAsync(userId, urlGetResource);

			return;
		}
		public async Task PrepareToUpdateETagForAllClientAsync(string urlGetResource)
		{

			ETag[] eTagTuples = await GetETagTuplesFromDBAsync(urlGetResource);

			Array.ForEach(eTagTuples, eTagTuple =>
				eTagTuple.ETagString = new ETagGenerator().GenerateRandom());

			return;
		}
		public async Task PrepareToUpdateETagForClientAsync(Guid userId,
			string urlGetResource)
		{
			ETag eTagTuple = await GetETagTupleFromDBAsync(userId, urlGetResource);

			if (eTagTuple != null)
			{
				eTagTuple.ETagString = new ETagGenerator().GenerateRandom();
			}

			return;
		}
		private static Guid GenerateOnePKForManyThreads(Guid userId, string urlGetResource)
		{
			byte[] hashPK = MD5.Create().ComputeHash(Encoding.Default.GetBytes(urlGetResource)
					.Concat(userId.ToByteArray()).ToArray());
			return new Guid(hashPK);
		}
		private ETag PrepareToCreateETagTuple(Guid userId, string urlGetResource)
		{
			ETag eTagTuple = new()
			{
				GetResourceId = GenerateOnePKForManyThreads(userId, urlGetResource),//concurrency insert protect
				ClientId = userId,
				UrlGetResource = urlGetResource
			};
			_eTagCacheContext.ETags.Add(eTagTuple);

			eTagTuple.ETagString = new ETagGenerator().GenerateRandom();

			return eTagTuple;
		}
		public async Task<string> GetETagFromDBCreateIfIsNullAsync(
			IEnumerable<Claim> userClaims, string urlGetResource)
		{
			Guid userId = GetUserId(userClaims);

			ETag eTagTuple = await GetETagTupleFromDBAsync(userId, urlGetResource);

			if (eTagTuple == null)
			{
				eTagTuple = PrepareToCreateETagTuple(userId, urlGetResource);
				await UpdateDBAsync();
			}

			return eTagTuple.ETagString;
		}
		private Guid GetUserId(IEnumerable<Claim> userClaims)
		{
			if (!_userService.TryGetUserId(userClaims, out Guid userId))
			{
				throw new ArgumentException("userId from cookie is not found");
			}
			return userId;
		}
		public async Task<ETag> GetETagTupleFromDBCreateIfIsNullAsync(
			IEnumerable<Claim> userClaims, string urlGetResource)
		{
			if (!_userService.TryGetUserId(userClaims, out Guid userId))
			{
				throw new ArgumentException("userId from cookie is not found");
			}

			ETag eTagTuple = await GetETagTupleFromDBAsync(userId, urlGetResource);

			eTagTuple ??= PrepareToCreateETagTuple(userId, urlGetResource);

			await UpdateDBAsync();

			return eTagTuple;
		}
	}
}
