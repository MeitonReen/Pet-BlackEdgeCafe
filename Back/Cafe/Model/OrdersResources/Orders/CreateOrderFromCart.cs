using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Databases.Cafe.Model;
using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.HandlersChain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.OrdersResources.Orders
{
	public class CreateOrderFromCart : HandlerBase
	{
		private readonly AppSettings _appSettings = null;
		private readonly CafeDatabase _cafeDB = null;

		#region params from chain request
		private Guid userId = Guid.Empty;
		private Databases.Cafe.Model.Cart clientCart = null;
		private DishesInCart[] dishesInClientCart = null;
		#endregion

		#region params to chain request
		private Guid newOrderId = Guid.Empty;
		#endregion

		public CreateOrderFromCart(AppSettings appSettings, CafeDatabase cafeDB)
		{
			_appSettings = appSettings;
			_cafeDB = cafeDB;
		}
		protected override void GetParamsFromChainRequest(ChainRequest request)
		{
			string className = GetType().Name;

			userId = GetParamFromChainRequest<Guid>(
				className, request, nameof(userId));
			clientCart = GetParamFromChainRequest<Databases.Cafe.Model.Cart>(
				className, request, nameof(clientCart));
			dishesInClientCart = GetParamFromChainRequest<DishesInCart[]>(
				className, request, nameof(dishesInClientCart));
			return;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			Order newOrder = new()
			{
				ClientId = userId,
				TableId = clientCart.TableId.Value,
				CookingStatus = clientCart.CookingStatus,
				DateTimeIsCompleted = DateTime.Now.AddSeconds(_appSettings
					.ValuesForPresentationMode.OrderLifeTimeSeconds),
				Amount = clientCart.Amount,
				AmountIncludingAppliedPromocodes = clientCart
					.AmountIncludingValidAppliedPromocodes
			};

			_cafeDB.Orders.Add(newOrder);
			await _cafeDB.SaveChangesAsync();

			newOrderId = newOrder.OrderId;

			_cafeDB.DishesInOrders.AddRange(dishesInClientCart.Select(DishInCart =>
				new DishesInOrder()
				{
					OrderId = newOrderId,
					ClientId = userId,
					DishId = DishInCart.DishId,
					CostIncludingAppliedPromocodes = DishInCart
						.CostIncludingValidAppliedPromocodes
				}));
			_cafeDB.AppliedPromocodesInOrders.AddRange(_cafeDB
				.AppliedPromocodesInCarts
				.Where(PromocodeInCart =>
					PromocodeInCart.CartId == clientCart.CartId)
				.Select(PromocodeInClientCart => new AppliedPromocodesInOrder()
				{
					PromocodeId = PromocodeInClientCart.PromocodeId,
					OrderId = newOrderId,
					ClientId = userId
				}));

			_cafeDB.Carts.Remove(clientCart);

			BookedTable bookedTableByClient = await _cafeDB.BookedTables
				.SingleOrDefaultAsync(bookedTable =>
					bookedTable.TableId == newOrder.TableId &&
					bookedTable.ClientId == userId &&
					bookedTable.DateTimeATableIsWillBeFree > DateTime.Now);
			if (bookedTableByClient != null)
			{
				bookedTableByClient.DateTimeATableIsWillBeFree = newOrder.DateTimeIsCompleted;
			}

			await _cafeDB.SaveChangesAsync();

			request.Status = ChainProcessingStatus.Success;
			return;
		}
		protected override void SetParamsToChainRequest(ChainRequest request)
		{
			request.Context.Add(nameof(newOrderId), newOrderId);
			return;
		}
	}
}