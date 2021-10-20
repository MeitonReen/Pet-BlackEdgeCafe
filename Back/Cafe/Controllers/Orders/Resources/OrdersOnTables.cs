
using Cafe.Infrastructure;
using Cafe.Infrastructure.ETagCache.Attributes;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Cafe.Model.OrdersResources.OrdersOnTables;
using Cafe.Model.Shared.Processing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cafe.Controllers.Orders
{
	public partial class OrdersController : ControllerBase
	{
		/// <summary>
		/// Get client orders on tables
		/// </summary>
		/// <response code="200">Return client orders on tables</response>
		[HttpGet]
		[Route(CafeAPIRoutes.V1.Orders.OrdersOnTables)]
		//[IgnoreAntiforgeryToken]
		[ETagCache]
		[ProducesResponseType(typeof(OrdersOnTableDTO[]), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetOrdersOnTables()
		{
			return await new HandlersChain()
				.AddChainLink(() => new UserIdToContext(_appSettings, HttpContext.User))
				.AddChainLink(() => new ReturnOrdersOnTablesV2Latest(_cafeDB))
				.RunChainAsync();
		}
	}
}
