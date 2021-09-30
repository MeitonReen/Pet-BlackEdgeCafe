using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.BookedTablesResources.BookedTables.Verificators
{
	public class IfUnbookingTableIsFound : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _tableId = Guid.Empty;

		#region params from chain request
		private Guid userId = Guid.Empty;
		#endregion

		#region params to chain request
		private BookedTable unbookATableByClient = null;
		#endregion

		public IfUnbookingTableIsFound(CafeDatabase cafeDB, Guid tableId)
		{
			_cafeDB = cafeDB;
			_tableId = tableId;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			userId = GetParamFromChainRequest<Guid>(
				this.GetType().Name, request, nameof(userId));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			unbookATableByClient = await _cafeDB.BookedTables.
				SingleOrDefaultAsync(Table => Table.TableId == _tableId &&
					Table.ClientId == userId &&
					Table.DateTimeATableIsWillBeFree > DateTime.Now);
			if (unbookATableByClient == default(BookedTable))
			{
				request.Status = ChainProcessingStatus.Failure_exit;
				request.Result = _resultGenerator.BadRequest(
					new ErrorDTO("Unbooking table is not found"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
		protected override void SetParamsToChainRequest(ChainRequest request)
		{
			request.Context.Add(nameof(unbookATableByClient), unbookATableByClient);
			return;
		}
	}
}