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
	public class ReturnOrdersOnTables : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Guid userId = Guid.Empty;
		#endregion

		public ReturnOrdersOnTables(CafeDatabase cafeDB)
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
			var Orders = await _cafeDB.Orders
				.Where(Order => Order.ClientId == userId)
				.Include(Order => Order.Table)
				.Include(Order => Order.DishesInOrders)
				.ThenInclude(DishesInOrders => DishesInOrders.Dish)
				.ToArrayAsync();

			OrdersOnTableDTO[] OrdersOnTables = Orders
				.GroupBy(Order => Order.Table)
				.Select(OrdersOnTable => new OrdersOnTableDTO(
					new TableDTO(OrdersOnTable.Key.TableId, OrdersOnTable.Key.TableNumber,
						OrdersOnTable.Key.NumberOfSeats),
						OrdersOnTable
							.Select(OrderOnTable => new OrderShortDTO(OrderOnTable.OrderId,
								OrderOnTable.DishesInOrders.Count, OrderOnTable.Amount,
								OrderOnTable.AmountIncludingAppliedPromocodes,
								OrderOnTable.CookingStatus,
								GetDishShortGroupedByDishIdFromOrder(OrderOnTable)))
							.ToArray()))
				.ToArray();

			request.Result = _resultGenerator.Ok(OrdersOnTables);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
		private static DishShortGroupedByDishIdDTO[] GetDishShortGroupedByDishIdFromOrder(
			Order order)
		{
			return order.DishesInOrders
				.GroupBy(Rec => new
				{
					Rec.DishId,
					Rec.CostIncludingAppliedPromocodes
				})
				.Select(GRec => new DishShortGroupedByDishIdDTO(GRec.Key.DishId,
					GRec.Key.CostIncludingAppliedPromocodes, GRec))
				.ToArray();
		}
	}
}