namespace Cafe.Model.DTOs
{
	public class NewQuantityDishesInCartDTO
	{
		public int NewQuantity { get; }

		public NewQuantityDishesInCartDTO(int newQuantity)
		{
			NewQuantity = newQuantity;
		}
	}
}
