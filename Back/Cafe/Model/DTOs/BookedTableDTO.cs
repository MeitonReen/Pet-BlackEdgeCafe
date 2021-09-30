using System;

namespace Cafe.Model.DTOs
{
	public class BookedTableDTO : TableDTO
	{
		public int BookedTableSeconds { get; }

		public BookedTableDTO(Guid tableId, int tableNumber, int numberOfSeats,
			int bookedTableSeconds)
		: base(tableId, tableNumber, numberOfSeats)
		{
			BookedTableSeconds = bookedTableSeconds;
		}
	}
}
