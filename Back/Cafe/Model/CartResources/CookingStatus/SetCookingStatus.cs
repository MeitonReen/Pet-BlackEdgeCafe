using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.HandlersChain;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.CookingStatus
{
	public class SetCookingStatus : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly string _newCookingStatus = null;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		#endregion

		public SetCookingStatus(CafeDatabase cafeDB, string newCookingStatus)
		{
			_cafeDB = cafeDB;
			_newCookingStatus = newCookingStatus;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				this.GetType().Name, request, nameof(clientCart));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			clientCart.CookingStatus = _newCookingStatus;
			await _cafeDB.SaveChangesAsync();

			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}