using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.BookedTablesResources.BookedTables
{
	public class ReturnBookedTablesByClientV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Guid userId = Guid.Empty;
		#endregion

		public ReturnBookedTablesByClientV2Latest(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			userId = GetParamFromChainRequest<Guid>(this.GetType().Name, request, nameof(userId));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			BookedTableDTO[] BookedTables = await (
				from bookedTable in _cafeDB.BookedTables
				where bookedTable.DateTimeATableIsWillBeFree > DateTime.Now &&
					bookedTable.ClientId == userId
				join table in _cafeDB.Tables on bookedTable.TableId equals table.TableId
				select new BookedTableDTO(bookedTable.TableId, table.TableNumber, table.NumberOfSeats,
					(bookedTable.DateTimeATableIsWillBeFree.Subtract(DateTime.Now).TotalSeconds > 0) ?
					(int)bookedTable.DateTimeATableIsWillBeFree.Subtract(DateTime.Now).TotalSeconds : 0)
			).ToArrayAsync();

			request.Result = _resultGenerator.Ok(BookedTables);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}