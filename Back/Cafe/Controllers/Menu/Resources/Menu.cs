using Cafe.Infrastructure;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Cafe.Model.MenuResources.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cafe.Controllers.Menu
{
	public partial class MenuController : ControllerBase
	{
		/// <summary>
		/// Get menu
		/// </summary>
		/// <response code="200">Return menu</response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.Menu.This)]
		[AllowAnonymous]
		[ETagCache]
		[ProducesResponseType(typeof(DishShortDTO[]), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetMenu()
		{
			return await new HandlersChain()
				.AddChainLink(() => new ReturnMenu(_cafeDB))
				.RunChainAsync();
		}
	}
}
