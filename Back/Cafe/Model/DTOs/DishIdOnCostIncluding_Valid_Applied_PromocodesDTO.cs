using System;

namespace Cafe.Model.DTOs
{
	public class DishIdOnCostIncluding_Valid_Applied_PromocodesDTO
	{
		public Guid DishId { get; }
		public float? CostIncluding_Valid_Applied_Promocodes { get; }

		public DishIdOnCostIncluding_Valid_Applied_PromocodesDTO(Guid dishId,
			float? costIncluding_Valid_Applied_PromocodesDTO)
		{
			DishId = dishId;
			CostIncluding_Valid_Applied_Promocodes = costIncluding_Valid_Applied_PromocodesDTO;
		}
	}
}
