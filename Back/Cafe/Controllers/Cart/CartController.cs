using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Cafe.Infrastructure;

namespace Cafe.Controllers.Cart
{
	[ApiController]
	[Route(CafeAPIRoutes.V1.This)]
	[Authorize]
	//[ValidateAntiForgeryToken]
	[Produces("application/json")]
	public partial class CartController : ControllerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly AppSettings _appSettings = null;

		public CartController(CafeDatabase cafeDB, AppSettings appSettings)
		{
			_cafeDB = cafeDB;
			_appSettings = appSettings;
		}
	}
}
