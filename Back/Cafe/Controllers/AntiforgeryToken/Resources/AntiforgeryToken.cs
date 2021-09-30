using Cafe.Model.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Controllers.AntiforgeryToken
{
	public partial class AntiforgeryTokenController : ControllerBase
	{
		/// <summary>Login</summary>
		/// <response code="204">Return antiforgery token pair: Set-Cookie: CSRF-Token-Cookie, header: X-CSRF-Token-Response</response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.AntiforgeryToken.This)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status204NoContent)]
		public IActionResult GetAntiforgeryToken()
		{
			var tokens = _antiforgery.GetAndStoreTokens(HttpContext);//set-cookie only if
																	 //there are no valid cookie
																	 //in request

			HttpContext.Response.Headers.Add(_appSettings.Constants
				.AntiforgeryTokenResponseHeaderName, tokens.RequestToken);

			return NoContent();
		}
	}
}
