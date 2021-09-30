
using Cafe.Infrastructure;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.CartResources.TableId;
using Cafe.Model.CartResources.TableId.Verificators;
using Cafe.Model.DTOs;
using Cafe.Model.Shared;
using Cafe.Model.Shared.Processing;
using Cafe.Model.Shared.Returns;
using Cafe.Model.Shared.Verificators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Cafe.Controllers.Cart
{
	public partial class CartController : ControllerBase
	{
		/// <summary>
		/// Get tableId from client cart
		/// </summary>
		/// <response code="404">Client cart is not found</response>
		/// <response code="200">Return tableId</response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.Cart.TableId)]
		//[IgnoreAntiforgeryToken]
		[ETagCache]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(TableIdDTO), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetTableId()
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfClientCartIsExists(_cafeDB))
				.AddChainLink(() => new ReturnTableId())
				.RunChainAsync();
		}
		/// <summary>
		/// Set tableId to client cart from booked tables
		/// </summary>
		/// <response code="400">Patching table is not a booked</response>
		/// <response code="404">Client cart is not found</response>
		/// <response code="200">Return tableId</response>
		[HttpPut]
		[Route(CafeAPIRoutes.V1.Cart.TableId)]
		[UpdateRelatedETags(CafeAPIRoutes.V1.This,
			CafeAPIRoutes.V1.Cart.This,
			CafeAPIRoutes.V1.Cart.MiniCart,
			CafeAPIRoutes.V1.Cart.TableId,
			CafeAPIRoutes.V1.BookedTables.This)]
		[Consumes(MimeTypes.Application.XWWWFormUrlencoded)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(TableIdDTO), StatusCodes.Status200OK)]
		public async Task<IActionResult> SetTableId([FromForm][Required] Guid tableId)
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfThisTableIdIsBookedByClient(_cafeDB, tableId))
				.AddChainLink(() => new IfClientCartIsExists(_cafeDB))
				.AddChainLink(() => new SetTableId(_cafeDB, tableId))
				.RunChainAsync();
		}
	}
}
