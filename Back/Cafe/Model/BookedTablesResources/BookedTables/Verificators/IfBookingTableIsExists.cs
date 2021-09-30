using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.BookedTablesResources.BookedTables.Verificators
{
	public class IfBookingTableIsExists : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _tableId = Guid.Empty;

		public IfBookingTableIsExists(CafeDatabase cafeDB, Guid tableId)
		{
			_cafeDB = cafeDB;
			_tableId = tableId;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			Table BookingTable = await _cafeDB.Tables
				.SingleOrDefaultAsync(Table => Table.TableId == _tableId);
			if (BookingTable == default(Table))
			{
				request.Status = ChainProcessingStatus.Failure_exit;
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Booking table is not found"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}