
using Cafe.Infrastructure;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.CartResources.Cart;
using Cafe.Model.DTOs;
using Cafe.Model.Shared.Processing;
using Cafe.Model.Shared.Returns;
using Cafe.Model.Shared.Verificators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cafe.Controllers.Cart
{
	public partial class CartController : ControllerBase
	{
		/// <summary>
		/// Create client cart
		/// </summary>
		/// <response code="200">Client cart exists</response>
		[HttpPost]
		[Route(CafeAPIRoutes.V1.Cart.This)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
		public async Task<IActionResult> CreateCart()
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new CreateCart(_cafeDB))
				.AddChainLink(() => new ReturnEmptyOkResult())
				.RunChainAsync();
		}
		/// <summary>
		/// Get client cart state
		/// </summary>
		/// <response code="404">Client cart not found</response>
		/// <response code="200">Return client cart state</response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.Cart.This)]
		//[IgnoreAntiforgeryToken]
		[ETagCache]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(CartStateDTO), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCartState()
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfClientCartIsExists(_cafeDB))
				.AddChainLink(() => new ReturnCartStateV2Latest(_cafeDB))
				.RunChainAsync();
		}
	}
}
