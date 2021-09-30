using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.Promocodes.Verificators
{
	public class IfThisPromocodeIsNotAddedToCart : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Databases.Cafe.Model.Cart clientCart = null;
		private Promocode addingPromocode = null;
		#endregion

		public IfThisPromocodeIsNotAddedToCart(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			string className = this.GetType().Name;

			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				className, request, nameof(clientCart));
			addingPromocode = GetParamFromChainRequest<Promocode>(
				className, request, nameof(addingPromocode));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			AppliedPromocodesInCart PromocodeInCart = await _cafeDB.AppliedPromocodesInCarts
				.SingleOrDefaultAsync(PIC => (PIC.CartId == clientCart.CartId) &&
					(PIC.PromocodeId == addingPromocode.PromocodeId));

			if (PromocodeInCart != default(AppliedPromocodesInCart))
			{
				request.Result = _resultGenerator.Ok();
				request.Status = ChainProcessingStatus.Success_exit;
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}