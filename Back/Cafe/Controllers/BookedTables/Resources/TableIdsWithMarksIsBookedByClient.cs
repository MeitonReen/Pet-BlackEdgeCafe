
using Cafe.Infrastructure;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.BookedTablesResources.IdsWithMarksIsBookedByClient;
using Cafe.Model.DTOs;
using Cafe.Model.Shared.Processing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cafe.Controllers.BookedTables
{
	public partial class BookedTablesController : ControllerBase
	{
		/// <summary>Get booked tables in short format</summary>
		/// <response code="200">Return booked tables ids with marks is booked by client</response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.BookedTables.TableIdsWithMarksIsBookedByClient)]
		//[IgnoreAntiforgeryToken]
		[ProducesResponseType(typeof(BookedTableIdWithMarkIsBookedByClientDTO[]), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetBookedTablesIdsWithMarksIsBookedByClient()
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new ReturnBookedTablesIdsWithMarksIsBookedByClient(_cafeDB))
				.RunChainAsync();
		}
	}
}
