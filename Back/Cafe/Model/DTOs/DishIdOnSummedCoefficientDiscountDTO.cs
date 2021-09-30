using System;

namespace Cafe.Model.DTOs
{
	public class DishIdOnSummedCoefficientDiscountDTO
	{
		public Guid DishId { get; }
		public float SummedCoefficientDiscount { get; }

		public DishIdOnSummedCoefficientDiscountDTO(Guid dishId,
			float summedCoefficientDiscount)
		{
			DishId = dishId;
			SummedCoefficientDiscount = summedCoefficientDiscount;
		}
	}
}
