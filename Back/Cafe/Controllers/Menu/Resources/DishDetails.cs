
using Cafe.Infrastructure;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Cafe.Model.MenuResources.DishDetails;
using Cafe.Model.MenuResources.DishDetails.Verificators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Cafe.Controllers.Menu
{
	public partial class MenuController : ControllerBase
	{
		/// <summary>
		/// Get dish details
		/// </summary>
		/// <param name="dishId"></param>
		/// <response code="400">Dish id is not found</response>
		/// <response code="200"></response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.Menu.DishDetails)]
		[AllowAnonymous]
		[ETagCache]
		[ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetDishDetails([FromRoute][Required] Guid dishId)
		{
			return await new HandlersChain()
				.AddChainLink(() => new IfThisDishIsExists(_cafeDB, dishId))
				.AddChainLink(() => new ReturnDishDetails(_cafeDB))
				.RunChainAsync();
		}
	}
}
