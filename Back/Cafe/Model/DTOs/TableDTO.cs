using System;

namespace Cafe.Model.DTOs
{
	public class TableDTO
	{
		public Guid TableId { get; }
		public int TableNumber { get; }
		public int NumberOfSeats { get; }

		public TableDTO(Guid tableId, int tableNumber, int numberOfSeats)
		{
			TableId = tableId;
			TableNumber = tableNumber;
			NumberOfSeats = numberOfSeats;
		}
	}
}
