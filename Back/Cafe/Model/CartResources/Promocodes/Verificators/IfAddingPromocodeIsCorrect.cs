using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cafe.Model.CartResources.Promocodes.Verificators
{
	public class IfAddingPromocodeIsCorrect : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;
		private readonly string _promocode = string.Empty;

		#region params to chain request
		private Promocode addingPromocode = null;
		#endregion

		public IfAddingPromocodeIsCorrect(CafeDatabase cafeDB, string promocode)
		{
			_cafeDB = cafeDB;
			_promocode = promocode;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			addingPromocode = await _cafeDB.Promocodes
				.SingleOrDefaultAsync(Promocode => Promocode.PromocodeText == _promocode &&
					Promocode.IsValid);
			if (addingPromocode == default(Promocode))
			{
				request.Status = ChainProcessingStatus.Failure_exit;
				request.Result = _resultGenerator
					.BadRequest(new ErrorDTO("Adding promocode is not found"));
				return;
			}
			request.Status = ChainProcessingStatus.Success;
			return;
		}
		protected override void SetParamsToChainRequest(ChainRequest request)
		{
			request.Context.Add(nameof(addingPromocode), addingPromocode);
			return;
		}
	}
}