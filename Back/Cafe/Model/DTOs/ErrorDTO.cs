namespace Cafe.Model.DTOs
{
	public class ErrorDTO
	{
		public string Message { get; }

		public ErrorDTO(string message)
		{
			Message = message;
		}
	}
}
