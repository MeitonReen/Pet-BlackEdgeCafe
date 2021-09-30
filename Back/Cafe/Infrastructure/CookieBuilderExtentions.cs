

using Microsoft.AspNetCore.Http;
using System;

namespace Cafe.Infrastructure
{
	public static class CookieBuilderExtentions
	{
		public static CookieOptions BuildFromSelf(this CookieBuilder cookieBuilder)
		{
			return new CookieOptions()
			{
				Domain = cookieBuilder.Domain,
				Expires = cookieBuilder.Expiration.HasValue ?
					DateTimeOffset.Now.Add(cookieBuilder.Expiration.GetValueOrDefault())
						: default(DateTimeOffset?),
				HttpOnly = cookieBuilder.HttpOnly,
				IsEssential = cookieBuilder.IsEssential,
				MaxAge = cookieBuilder.MaxAge,
				Path = cookieBuilder.Path,
				SameSite = cookieBuilder.SameSite,
				Secure = cookieBuilder.SecurePolicy == CookieSecurePolicy.Always
			};
		}
	}
}
