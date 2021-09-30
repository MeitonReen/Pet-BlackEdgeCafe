using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.BookedTablesResources.BookedTables.Verificators
{
	public class IfBookingTableIsNotAlreadyBooked : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _tableId = Guid.Empty;

		public IfBookingTableIsNotAlreadyBooked(CafeDatabase cafeDB, Guid tableId)
		{
			_cafeDB = cafeDB;
			_tableId = tableId;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			BookedTable BookingTableIsBooked = await _cafeDB.BookedTables
				.SingleOrDefaultAsync(Table => Table.TableId == _tableId &&
					Table.DateTimeATableIsWillBeFree > DateTime.Now);

			if (BookingTableIsBooked != default(BookedTable))
			{
				request.Status = ChainProcessingStatus.Failure_exit;
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Table already is booked"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}