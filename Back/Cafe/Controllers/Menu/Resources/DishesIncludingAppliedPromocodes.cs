
using Cafe.Infrastructure;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Cafe.Model.MenuResources.MenuIncludingAppliedPromocodes;
using Cafe.Model.Shared.Processing;
using Cafe.Model.Shared.Verificators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cafe.Controllers.Menu
{
	public partial class MenuController : ControllerBase
	{
		/// <summary>
		/// Get menu including promocodes
		/// </summary>
		/// <response code="404">Client cart is not found</response>
		/// <response code="200">Return menu including promocodes applied in cart</response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.Menu.DishesIncludingAppliedPromocodes)]
		[ETagCache]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(DishShortDTO[]), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetMenuIncludingPromocodes()
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new IfClientCartIsExists(_cafeDB))
				.AddChainLink(() => new ReturnMenuIncludingPromocodesV2Latest(_cafeDB))
				.RunChainAsync();
		}
	}
}
