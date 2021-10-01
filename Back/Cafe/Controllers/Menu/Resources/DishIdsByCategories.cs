
using Cafe.Infrastructure;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Cafe.Model.MenuResources.DishIdsByCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cafe.Controllers.Menu
{
	public partial class MenuController : ControllerBase
	{
		/// <summary>
		/// Get dishids by categories
		/// </summary>
		/// <response code="200">Return dishids by categories</response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.Menu.DishIdsByCategories)]
		[AllowAnonymous]
		[ETagCache]
		[ProducesResponseType(typeof(DishIdOnCategoriesDTO[]), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetDishIdsByCategories()
		{
			return await new HandlersChain()
				.AddChainLink(() => new ReturnDishIdsByCategories(_cafeDB))
				.RunChainAsync();
		}
	}
}
