
using Cafe.Infrastructure;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.CartResources.Dishes;
using Cafe.Model.CartResources.Dishes.Verificators;
using Cafe.Model.DTOs;
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
		/// Add dish to cart
		/// </summary>
		/// <param name="dishId"></param>
		/// <response code="404">Client cart is not found</response> 
		/// <response code="400">Adding dish is not found</response> 
		/// <response code="200"></response> 
		[HttpPost]
		[Route(CafeAPIRoutes.V1.Cart.Dishes)]
		[UpdateRelatedETags(CafeAPIRoutes.V1.This,
			CafeAPIRoutes.V1.Cart.This,
			CafeAPIRoutes.V1.Cart.MiniCart)]
		[Consumes(MimeTypes.Application.XWWWFormUrlencoded)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
		public async Task<IActionResult> AddDish([FromForm][Required] Guid dishId)
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfClientCartIsExists(_cafeDB))
				.AddChainLink(() => new IfAddingDishIsExists(_cafeDB, dishId))
				.AddChainLink(() => new ApplySummedAppliedValidPromocodesOnDish(_cafeDB, dishId))
				.AddChainLink(() => new AddDish(_cafeDB, dishId))
				.AddChainLink(() => new ReturnEmptyOkResult())
				.RunChainAsync();
		}
		/// <summary>
		/// Delete dishes by dishid from client cart
		/// </summary>
		/// <param name="dishId"></param>
		/// <response code="404">Client cart is not found</response>
		/// <response code="400">Deleting dishes is not found</response>
		/// <response code="200"></response>
		[HttpDelete]
		[Route(CafeAPIRoutes.V1.Cart.Dishes)]
		[UpdateRelatedETags(CafeAPIRoutes.V1.This,
			CafeAPIRoutes.V1.Cart.This,
			CafeAPIRoutes.V1.Cart.MiniCart)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteDishesByDishId(
			[FromForm][Required] Guid dishId)
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfClientCartIsExists(_cafeDB))
				.AddChainLink(() => new IfDeletingDishesByDishIdIsExists(_cafeDB, dishId))
				.AddChainLink(() => new ApplySummedAppliedValidPromocodesOnDish(_cafeDB, dishId))
				.AddChainLink(() => new DeleteDishesByDishId(_cafeDB, dishId))
				.AddChainLink(() => new ReturnEmptyOkResult())
				.RunChainAsync();
		}
	}
}
