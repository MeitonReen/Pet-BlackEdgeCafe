
using Cafe.Infrastructure;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.CartResources.MiniCart;
using Cafe.Model.DTOs;
using Cafe.Model.Shared.Processing;
using Cafe.Model.Shared.Verificators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cafe.Controllers.Cart
{
	public partial class CartController : ControllerBase
	{
		/// <summary>
		/// Get mini cart state
		/// </summary>
		/// <response code="404">Client cart is not found</response>
		/// <response code="200">Return mini cart state</response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.Cart.MiniCart)]
		//[IgnoreAntiforgeryToken]
		[ETagCache]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(MiniCartStateDTO), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetMiniCartState()
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfClientCartIsExists(_cafeDB))
				.AddChainLink(() => new ReturnMiniCartState(_cafeDB))
				.RunChainAsync();
		}
	}
}
