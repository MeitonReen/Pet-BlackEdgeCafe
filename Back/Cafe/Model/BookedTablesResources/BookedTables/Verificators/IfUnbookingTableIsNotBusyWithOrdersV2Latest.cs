using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.BookedTablesResources.BookedTables.Verificators
{
	public class IfUnbookingTableIsNotBusyWithOrdersV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private BookedTable unbookATableByClient = null;
		#endregion

		public IfUnbookingTableIsNotBusyWithOrdersV2Latest(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			unbookATableByClient = GetParamFromChainRequest<BookedTable>(
				this.GetType().Name, request, nameof(unbookATableByClient));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			Order[] Orders = await (
				from order in _cafeDB.Orders
				where order.TableId == unbookATableByClient.TableId &&
					DateTime.Now < order.DateTimeIsCompleted
				select order
			).ToArrayAsync();

			if (Orders.Any())
			{
				request.Status = ChainProcessingStatus.Failure_exit;
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Unbooking table is busy with orders"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}