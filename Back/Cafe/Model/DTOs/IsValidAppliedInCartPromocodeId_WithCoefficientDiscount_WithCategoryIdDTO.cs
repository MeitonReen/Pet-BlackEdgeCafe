using System;

namespace Cafe.Model.DTOs
{
	public class IsValidAppliedInCartPromocodeId_WithCoefficientDiscount_WithCategoryIdDTO
	{
		public Guid PromocodeId { get; }
		public float CoefficientDiscount { get; }
		public Guid CategoryId { get; }

		public IsValidAppliedInCartPromocodeId_WithCoefficientDiscount_WithCategoryIdDTO(
			Guid promocodeId,
			float coefficientDiscount,
			Guid categoryId)
		{
			PromocodeId = promocodeId;
			CoefficientDiscount = coefficientDiscount;
			CategoryId = categoryId;
		}
	}
}
