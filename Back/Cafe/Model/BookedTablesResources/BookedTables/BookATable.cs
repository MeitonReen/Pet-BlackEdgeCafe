using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.HandlersChain;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.BookedTablesResources.BookedTables
{
	public class BookATable : HandlerBase
	{
		private readonly AppSettings _appSettings = null;
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _tableId = Guid.Empty;

		#region params from chain request
		private Guid userId = Guid.Empty;
		#endregion

		public BookATable(AppSettings appSettings, CafeDatabase cafeDB,
			Guid tableId)
		{
			_appSettings = appSettings;
			_cafeDB = cafeDB;
			_tableId = tableId;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			userId = GetParamFromChainRequest<Guid>(GetType().Name, request,
				nameof(userId));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			_cafeDB.BookedTables.Add(new BookedTable()
			{
				ClientId = userId,
				TableId = _tableId,
				DateTimeATableIsWillBeFree = DateTime.Now.AddMinutes(_appSettings
					.ValuesForPresentationMode.BookedATableMinutes)
			});
			await _cafeDB.SaveChangesAsync();

			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}