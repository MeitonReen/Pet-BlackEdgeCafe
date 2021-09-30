
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.CartResources.Promocodes;
using Cafe.Model.CartResources.Promocodes.Verificators;
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
		/// Add promocode to client cart
		/// </summary>
		/// <param name="promocode"></param>
		/// <response code="404">Client cart is not found</response>
		/// <response code="400">Adding promocode is not found</response>
		/// <response code="200">Promocode is already in cart;
		/// Promocode added to cart</response>
		[Route(CafeAPIRoutes.V1.Cart.Promocodes)]
		[HttpPost]
		[UpdateRelatedETags(CafeAPIRoutes.V1.This,
			CafeAPIRoutes.V1.Cart.This,
			CafeAPIRoutes.V1.Cart.MiniCart,
			CafeAPIRoutes.V1.Menu.DishesIncludingAppliedPromocodes)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
		public async Task<IActionResult> AddPromocode([FromForm][Required] string promocode)
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfClientCartIsExists(_cafeDB))
				.AddChainLink(() => new IfAddingPromocodeIsCorrect(_cafeDB, promocode))
				.AddChainLink(() => new IfThisPromocodeIsNotAddedToCart(_cafeDB))
				.AddChainLink(() => new AddPromocodeToCart(_cafeDB))
				.AddChainLink(() => new ReturnEmptyOkResult())
				.RunChainAsync();
		}
	}
}
