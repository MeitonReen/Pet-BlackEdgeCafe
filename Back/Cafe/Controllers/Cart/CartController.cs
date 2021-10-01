using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Model.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

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
