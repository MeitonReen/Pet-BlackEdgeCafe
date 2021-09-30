namespace Cafe.Model.DTOs
{
	public class OrdersOnTableDTO
	{
		public TableDTO Table { get; }
		public OrderShortDTO[] Orders { get; }

		public OrdersOnTableDTO(TableDTO table, OrderShortDTO[] orders)
		{
			Table = table;
			Orders = orders;
		}
	}
}
