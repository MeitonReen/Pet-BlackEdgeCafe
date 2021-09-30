namespace Cafe.Infrastructure.ApplicationSettings
{
	public class Databases
	{
		public CafeDBTypesDefault Cafe { get; init; }
		public CafeDBTypesDefault Identity { get; init; }
		public CafeDBTypesDefault ETagCache { get; init; }
	}
}