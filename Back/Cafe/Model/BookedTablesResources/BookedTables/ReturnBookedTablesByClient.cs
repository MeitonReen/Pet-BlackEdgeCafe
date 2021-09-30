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
	public class ReturnBookedTablesByClient : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Guid userId = Guid.Empty;
		#endregion

		public ReturnBookedTablesByClient(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			userId = GetParamFromChainRequest<Guid>(this.GetType().Name, request,
				nameof(userId));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			BookedTableDTO[] BookedTables = await _cafeDB.BookedTables
				.Where(BookedTable =>
					BookedTable.DateTimeATableIsWillBeFree > DateTime.Now &&
					BookedTable.ClientId == userId)
				.Join(_cafeDB.Tables, BT => BT.TableId, T => T.TableId, (BT, T) =>
					new BookedTableDTO(BT.TableId, T.TableNumber, T.NumberOfSeats,
						(BT.DateTimeATableIsWillBeFree.Subtract(DateTime.Now).TotalSeconds > 0) ?
							(int)BT.DateTimeATableIsWillBeFree.Subtract(DateTime.Now).TotalSeconds : 0))
				.ToArrayAsync();

			request.Result = _resultGenerator.Ok(BookedTables);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}