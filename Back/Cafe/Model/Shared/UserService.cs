using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Cafe.Model.Shared
{
	public class UserService
	{
		private readonly AppSettings _appSettings = null;

		public UserService(AppSettings appSettings)
		{
			_appSettings = appSettings;
		}
		public static void UnsetCookie(HttpResponse httpResponse, string cookieName,
			CookieOptions cookieOptions)
		{
			cookieOptions.MaxAge = null;
			cookieOptions.Expires = DateTimeOffset.Now.AddSeconds(-1);

			httpResponse.Cookies.Append(cookieName, string.Empty, cookieOptions);
			httpResponse.Headers.Add(HeaderNames.CacheControl, new StringValues(
				new string[]
				{
					CacheControlHeaderValue.NoCacheString,
					CacheControlHeaderValue.NoStoreString
				}));
			httpResponse.Headers.Add(HeaderNames.Pragma, new StringValues(
				CacheControlHeaderValue.NoCacheString));
			
		}
		public Claim CreateUserIdClaim(string id)
		{
			byte[] hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(id));
			string userId = new Guid(hash).ToString();

			return new Claim(_appSettings.Constants.UserId, userId);
		}
		public bool UserIdExists(IEnumerable<Claim> claims)
		{
			return claims.SingleOrDefault(Claim => Claim.Type == _appSettings.Constants.UserId)
				!= null;
		}
		public bool TryGetUserId(IEnumerable<Claim> claims, out Guid userId)
		{
			return Guid.TryParse(claims.SingleOrDefault(Claim =>
				Claim.Type == _appSettings.Constants.UserId)
					?.Value, out userId);
		}
		public bool TryGetUserIdAndUserName(IEnumerable<Claim> claims, out string userIdUserName)
		{
			Guid.TryParse(claims.SingleOrDefault(Claim =>
				Claim.Type == _appSettings.Constants.UserId)?.Value, out var userId);
			string userName = claims.SingleOrDefault(Claim =>
				Claim.Type == _appSettings.Constants.UserName)?.Value;

			bool success = userId != Guid.Empty && userName != null;

			userIdUserName = success ? $"userName=[{userName}]; id=[{userId}]" : null;

			return success;
		}
	}
}
