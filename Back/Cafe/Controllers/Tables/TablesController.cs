using Cafe.Databases.Cafe.Context.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Cafe.Infrastructure;

namespace Cafe.Controllers.Tables
{
	[ApiController]
	[Route(CafeAPIRoutes.V1.This)]
	[Produces("application/json")]
	public partial class TablesController : ControllerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		public TablesController(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
	}
}
