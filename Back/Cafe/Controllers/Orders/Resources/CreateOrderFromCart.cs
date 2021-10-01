
using Cafe.Infrastructure;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.ETagCache.Attributes.AttributeSpecParams;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Cafe.Model.OrdersResources.Orders;
using Cafe.Model.OrdersResources.Orders.Verificators;
using Cafe.Model.Shared.Processing;
using Cafe.Model.Shared.Verificators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cafe.Controllers.Orders
{
	public partial class OrdersController : ControllerBase
	{
		/// <summary>
		/// Create order from cart
		/// </summary>
		/// <response code="404">Client cart is not found</response>
		/// <response code="400">Table in client cart is not selected;
		/// No dishes in client cart</response>
		/// <response code="201">Return created orderid (without header location)</response>
		[HttpPut]
		[Route(CafeAPIRoutes.V1.Orders.CreateOrderFromCart)]
		[UpdateRelatedETags(CafeAPIRoutes.V1.This,
			CafeAPIRoutes.V1.Cart.This,
			CafeAPIRoutes.V1.Cart.CookingStatus,
			CafeAPIRoutes.V1.Cart.MiniCart,
			CafeAPIRoutes.V1.Cart.TableId,
			CafeAPIRoutes.V1.Menu.DishesIncludingAppliedPromocodes,
			CafeAPIRoutes.V1.Orders.OrdersOnTables)]
		[UpdateRelatedETags(CafeAPIRoutes.V1.This,
			CafeAPIRoutes.V1.BookedTables.This,
			CafeAPIRoutes.V1.BookedTables.TableIdsWithMarksIsBookedByClient,
			UpdateRelatedETagsFor = UpdateRelatedETagsFor.AllClient)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(OrderIdDTO), StatusCodes.Status201Created)]
		public async Task<IActionResult> CreateOrderFromCart()
		{
			var claims = HttpContext.User.Claims;

			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfClientCartIsExists(_cafeDB))
				.AddChainLink(() => new IfTableSelectedInCart())
				.AddChainLink(() => new IDishesExistsInCart(_cafeDB))
				.AddChainLink(() => new CreateOrderFromCart(_appSettings, _cafeDB))
				.AddChainLink(() => new ReturnNewOrderId())
				.RunChainAsync();
		}
	}
}
