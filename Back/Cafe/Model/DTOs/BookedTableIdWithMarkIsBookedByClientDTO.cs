using System;

namespace Cafe.Model.DTOs
{
	public class BookedTableIdWithMarkIsBookedByClientDTO
	{
		public Guid TableId { get; }
		public bool IsBookedByClient { get; }
		public int BookedTableSeconds { get; }

		public BookedTableIdWithMarkIsBookedByClientDTO(Guid tableId, bool isBookedByClient,
			int bookedTableSeconds)
		{
			TableId = tableId;
			IsBookedByClient = isBookedByClient;
			BookedTableSeconds = bookedTableSeconds;
		}
	}
}
