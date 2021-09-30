using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.HandlersChain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.Cart
{
	public class CreateCart : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Guid userId = Guid.Empty;
		#endregion

		public CreateCart(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			userId = GetParamFromChainRequest<Guid>(GetType().Name,
				request, nameof(userId));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			Databases.Cafe.Model.Cart ClientCart = null;
			ClientCart = await _cafeDB.Carts
					.SingleOrDefaultAsync(Cart => Cart.ClientId == userId);

			if (ClientCart == default(Databases.Cafe.Model.Cart))
			{
				_cafeDB.Carts.Add(new Databases.Cafe.Model.Cart()
				{
					CartId = userId,//concurrency insert protect
					ClientId = userId,
					Amount = 0,
					AmountIncludingValidAppliedPromocodes = 0
				});

				await _cafeDB.SaveChangesAsync();
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}