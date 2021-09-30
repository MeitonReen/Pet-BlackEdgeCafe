using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Cafe.Model.Shared;
using Cafe.Model.TablesResources.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cafe.Controllers.Tables
{
	public partial class TablesController : ControllerBase
	{
		/// <summary>
		/// Get all tables
		/// </summary>
		/// <response code="200">Return tables</response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.Tables.This)]
		[ETagCache]
		[ProducesResponseType(typeof(TableDTO[]), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetTables()
		{
			return await new HandlersChain()
				.AddChainLink(() => new ReturnTables(_cafeDB))
				.RunChainAsync();
		}
	}
}
