using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.BookedTablesResources.IdsWithMarksIsBookedByClient
{
	public class ReturnBookedTablesIdsWithMarksIsBookedByClient : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Guid userId = Guid.Empty;
		#endregion

		public ReturnBookedTablesIdsWithMarksIsBookedByClient(CafeDatabase cafeDB)
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
			BookedTableIdWithMarkIsBookedByClientDTO[] bookedTables = await _cafeDB.BookedTables
				.Where(bookedTable => bookedTable.DateTimeATableIsWillBeFree > DateTime.Now)
				.Select(bookedTable => new BookedTableIdWithMarkIsBookedByClientDTO(
					bookedTable.TableId, bookedTable.ClientId == userId,
					bookedTable.DateTimeATableIsWillBeFree.Subtract(DateTime.Now)
						.TotalSeconds > 0 ?
						(int)bookedTable.DateTimeATableIsWillBeFree.Subtract(DateTime.Now)
							.TotalSeconds : 0))
				.ToArrayAsync();

			request.Result = _resultGenerator.Ok(bookedTables);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}