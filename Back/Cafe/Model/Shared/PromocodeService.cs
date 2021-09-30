namespace Cafe.Model.Shared
{
	public class PromocodeService
	{
		public float? ApplyPromocode(int cost, float coefficientDiscount)
		{
			return cost * (1 - coefficientDiscount);
		}
		public float NormalizeCoefficientDiscount(float coefficientDiscount)
		{
			return coefficientDiscount > 1 ? 1 : coefficientDiscount;
		}
	}
}
