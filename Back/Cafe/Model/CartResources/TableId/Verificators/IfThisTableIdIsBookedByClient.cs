using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.TableId.Verificators
{
	public class IfThisTableIdIsBookedByClient : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _tableId = Guid.Empty;

		#region params from chain request
		private Guid userId = Guid.Empty;
		#endregion

		public IfThisTableIdIsBookedByClient(CafeDatabase cafeDB, Guid tableId)
		{
			_cafeDB = cafeDB;
			_tableId = tableId;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			userId = GetParamFromChainRequest<Guid>(
				GetType().Name, request, nameof(userId));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			BookedTable CheckBookedTable = await _cafeDB.BookedTables
				.SingleOrDefaultAsync(BT => BT.TableId == _tableId &&
					BT.DateTimeATableIsWillBeFree > DateTime.Now &&
					BT.ClientId == userId);
			if (CheckBookedTable == default(BookedTable))
			{
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("This table is not a booked by client"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}