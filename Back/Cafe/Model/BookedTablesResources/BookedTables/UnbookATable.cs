using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.BookedTablesResources.BookedTables
{
	public class UnbookATable : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private BookedTable unbookATableByClient = null;
		private Guid userId = Guid.Empty;
		#endregion

		public UnbookATable(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			string className = this.GetType().Name;

			unbookATableByClient = GetParamFromChainRequest<BookedTable>(
				className, request, nameof(unbookATableByClient));
			userId = GetParamFromChainRequest<Guid>(
				className, request, nameof(userId));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			Cart ClientCart = await _cafeDB.Carts
				.SingleOrDefaultAsync(Cart => Cart.ClientId == userId);

			if (ClientCart != default(Cart) &&
				ClientCart.TableId == unbookATableByClient.TableId)
			{
				ClientCart.TableId = null;
			}

			_cafeDB.BookedTables.Remove(unbookATableByClient);

			await _cafeDB.SaveChangesAsync();

			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}