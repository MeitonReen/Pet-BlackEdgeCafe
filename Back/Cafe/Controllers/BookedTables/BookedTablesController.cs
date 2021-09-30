using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Model.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Controllers.BookedTables
{
	[ApiController]
	[Route(CafeAPIRoutes.V1.This)]
	
	[Authorize]
	//[ValidateAntiForgeryToken]
	[Produces("application/json")]
	public partial class BookedTablesController : ControllerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly AppSettings _appSettings = null;

		public BookedTablesController(CafeDatabase cafeDB, AppSettings appSettings)
		{
			_cafeDB = cafeDB;
			_appSettings = appSettings;
		}
	}
}
