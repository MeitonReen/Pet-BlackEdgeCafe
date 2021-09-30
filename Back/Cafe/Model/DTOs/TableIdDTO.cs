using System;

namespace Cafe.Model.DTOs
{
	public class TableIdDTO
	{
		public Guid? TableId { get; }

		public TableIdDTO(Guid? tableId)
		{
			TableId = tableId;
		}
	}
}
