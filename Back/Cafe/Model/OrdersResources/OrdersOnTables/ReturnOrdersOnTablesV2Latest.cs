using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.OrdersResources.OrdersOnTables
{
	public class ReturnOrdersOnTablesV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Guid userId = Guid.Empty;
		#endregion

		public ReturnOrdersOnTablesV2Latest(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			userId = GetParamFromChainRequest<Guid>(
				GetType().Name, request, nameof(userId));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			var Orders = await (
				from order in _cafeDB.Orders
				where order.ClientId == userId
				select order
			).Include(Order => Order.Table)
				.Include(Order => Order.DishesInOrders)
				.ThenInclude(DishesInOrders => DishesInOrders.Dish)
				.ToArrayAsync();

			OrdersOnTableDTO[] OrdersOnTables = (
				from order in Orders
				group order by order.Table
			).Select(OrdersOnTable => new OrdersOnTableDTO(
				new TableDTO(OrdersOnTable.Key.TableId, OrdersOnTable.Key.TableNumber,
					OrdersOnTable.Key.NumberOfSeats), (
						from orderOnTable in OrdersOnTable
						select new OrderShortDTO(orderOnTable.OrderId,
							orderOnTable.DishesInOrders.Count, orderOnTable.Amount,
							orderOnTable.AmountIncludingAppliedPromocodes,
							orderOnTable.CookingStatus,
							GetDishShortGroupedByDishIdFromOrder(orderOnTable))
					).ToArray())
			).ToArray();

			request.Result = _resultGenerator.Ok(OrdersOnTables);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
		private static DishShortGroupedByDishIdDTO[] GetDishShortGroupedByDishIdFromOrder(
			Order order)
		{
			return (
				from dishInOrder in order.DishesInOrders
				group dishInOrder by new
				{
					dishInOrder.DishId,
					dishInOrder.CostIncludingAppliedPromocodes
				}
			).Select(gRec => new DishShortGroupedByDishIdDTO(gRec.Key.DishId,
				gRec.Key.CostIncludingAppliedPromocodes, gRec))
			.ToArray();
		}
	}
}