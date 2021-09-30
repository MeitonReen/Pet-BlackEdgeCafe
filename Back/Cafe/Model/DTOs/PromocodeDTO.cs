using System;

namespace Cafe.Model.DTOs
{
	public class PromocodeDTO
	{
		public Guid PromocodeId { get; }
		public string PromocodeText { get; }

		public PromocodeDTO(Guid promocodeId, string promocodeText)
		{
			PromocodeId = promocodeId;
			PromocodeText = promocodeText;
		}
	}
}
