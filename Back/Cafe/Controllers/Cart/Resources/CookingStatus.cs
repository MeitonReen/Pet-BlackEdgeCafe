
using Cafe.Infrastructure;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.CartResources.CookingStatus;
using Cafe.Model.DTOs;
using Cafe.Model.Shared;
using Cafe.Model.Shared.Processing;
using Cafe.Model.Shared.Returns;
using Cafe.Model.Shared.Verificators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Cafe.Controllers.Cart
{
	public partial class CartController : ControllerBase
	{
		/// <summary>
		/// Set cooking status to cart
		/// </summary>
		/// <param name="newCookingStatus"></param>
		/// <response code="404">Client cart is not found</response>
		/// <response code="201">New cooking status set to cart</response>
		[HttpPut]
		[Route(CafeAPIRoutes.V1.Cart.CookingStatus)]
		[UpdateRelatedETags(CafeAPIRoutes.V1.This,
			CafeAPIRoutes.V1.Cart.This,
			CafeAPIRoutes.V1.Cart.CookingStatus,
			CafeAPIRoutes.V1.Cart.MiniCart)]
		[Consumes(MimeTypes.Application.XWWWFormUrlencoded)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status201Created)]
		public async Task<IActionResult> SetCookingStatus(
			[FromForm][Required] string newCookingStatus)
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfClientCartIsExists(_cafeDB))
				.AddChainLink(() => new SetCookingStatus(_cafeDB, newCookingStatus))
				.AddChainLink(() => new ReturnEmptyCreatedResult())
				.RunChainAsync();
		}
		/// <summary>
		/// Get cooking status from cart
		/// </summary>
		/// <response code="404">Client cart is not found</response>
		/// <response code="200">Return cooking status from client cart</response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.Cart.CookingStatus)]
		//[IgnoreAntiforgeryToken]
		[ETagCache]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(CookingStatusDTO), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCookingStatus()
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfClientCartIsExists(_cafeDB))
				.AddChainLink(() => new ReturnCookingStatus())
				.RunChainAsync();
		}
	}
}
