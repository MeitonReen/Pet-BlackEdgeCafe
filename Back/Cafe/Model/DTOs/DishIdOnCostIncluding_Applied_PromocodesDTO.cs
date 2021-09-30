using System;

namespace Cafe.Model.DTOs
{
	public class DishIdOnCostIncluding_Applied_PromocodesDTO
	{
		public Guid DishId { get; }
		public float? CostIncluding_Applied_PromocodesDTO { get; }

		public DishIdOnCostIncluding_Applied_PromocodesDTO(Guid dishId,
			float? costIncluding_Applied_PromocodesDTO)
		{
			DishId = dishId;
			CostIncluding_Applied_PromocodesDTO = costIncluding_Applied_PromocodesDTO;
		}
	}
}
