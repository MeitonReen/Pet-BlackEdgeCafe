using Cafe.Infrastructure;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.ETagCache.Attributes.AttributeSpecParams;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.BookedTablesResources.BookedTables;
using Cafe.Model.BookedTablesResources.BookedTables.Verificators;
using Cafe.Model.DTOs;
using Cafe.Model.Shared;
using Cafe.Model.Shared.Processing;
using Cafe.Model.Shared.Returns;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Cafe.Controllers.BookedTables
{
	public partial class BookedTablesController : ControllerBase
	{
		/// <summary>Get booked tables</summary>
		/// <response code="200">Return booked tables</response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.BookedTables.This)]
		//[IgnoreAntiforgeryToken]
		[ETagCache]
		[ProducesResponseType(typeof(TableDTO[]), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetBookedTables()
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new ReturnBookedTablesByClient(_cafeDB))
				.RunChainAsync();
		}
		/// <summary>Book a table</summary>
		/// <param name="tableId"></param>
		/// <response code="400">Booking table is not found/
		///Table already is booked</response>
		/// <response code="200"></response>
		[HttpPost]
		[Route(CafeAPIRoutes.V1.BookedTables.This)]
		[UpdateRelatedETags(CafeAPIRoutes.V1.This,
			CafeAPIRoutes.V1.BookedTables.This,
			CafeAPIRoutes.V1.BookedTables.TableIdsWithMarksIsBookedByClient,
			UpdateRelatedETagsFor = UpdateRelatedETagsFor.AllClient)]
		[Consumes(MimeTypes.Application.XWWWFormUrlencoded)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
		public async Task<IActionResult> BookATable([FromForm][Required] Guid tableId)
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfBookingTableIsExists(_cafeDB, tableId))
				.AddChainLink(() => new IfBookingTableIsNotAlreadyBooked(_cafeDB, tableId))
				.AddChainLink(() => new BookATable(_appSettings, _cafeDB, tableId))
				.AddChainLink(() => new ReturnEmptyOkResult())
				.RunChainAsync();
		}
		/// <summary>Unbook a table</summary>
		/// <param name="tableId"></param>
		/// <response code="400">Unbooking table is not found;
		///Unbooking table is busy with orders</response>
		/// <response code="200"></response>
		[HttpDelete]
		[Route(CafeAPIRoutes.V1.BookedTables.This)]
		[UpdateRelatedETags(CafeAPIRoutes.V1.This,
			CafeAPIRoutes.V1.BookedTables.This,
			CafeAPIRoutes.V1.BookedTables.TableIdsWithMarksIsBookedByClient,
			UpdateRelatedETagsFor = UpdateRelatedETagsFor.AllClient)]
		[Consumes(MimeTypes.Application.XWWWFormUrlencoded)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]

		public async Task<IActionResult> UnbookATable([FromRoute][Required] Guid tableId)
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfUnbookingTableIsFound(_cafeDB, tableId))
				.AddChainLink(() => new IfUnbookingTableIsNotBusyWithOrders(_cafeDB))
				.AddChainLink(() => new UnbookATable(_cafeDB))
				.AddChainLink(() => new ReturnEmptyOkResult())
				.RunChainAsync();
		}
	}
}
