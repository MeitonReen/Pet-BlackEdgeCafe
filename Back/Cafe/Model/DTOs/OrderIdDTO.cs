using System;

namespace Cafe.Model.DTOs
{
	public class OrderIdDTO
	{
		public Guid? OrderId { get; }

		public OrderIdDTO(Guid? orderId)
		{
			OrderId = orderId;
		}
	}
}
