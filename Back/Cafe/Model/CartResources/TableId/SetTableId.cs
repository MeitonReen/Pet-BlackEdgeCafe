using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.TableId
{
	public class SetTableId : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly Guid _tableId = Guid.Empty;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		#endregion

		public SetTableId(CafeDatabase cafeDB, Guid tableId)
		{
			_cafeDB = cafeDB;
			_tableId = tableId;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				this.GetType().Name, request, nameof(clientCart));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			clientCart.TableId = _tableId;
			await _cafeDB.SaveChangesAsync();

			request.Result = _resultGenerator.Ok(new TableIdDTO(_tableId));
			request.Status = ChainProcessingStatus.Success_exit;
			return;
		}
	}
}