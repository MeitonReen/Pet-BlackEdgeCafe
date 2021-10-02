using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Cafe.Infrastructure;

namespace Cafe.Controllers.Menu
{
	[ApiController]
	[Route(CafeAPIRoutes.V1.This)]
	[Authorize]
	[Produces("application/json")]
	public partial class MenuController : ControllerBase
	{
		private readonly AppSettings _appSettings = null;
		private readonly CafeDatabase _cafeDB = null;

		public MenuController(AppSettings appSettings, CafeDatabase cafeDB)
		{
			_appSettings = appSettings;
			_cafeDB = cafeDB;
		}
	}
}
