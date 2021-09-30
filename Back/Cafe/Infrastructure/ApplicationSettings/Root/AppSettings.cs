namespace Cafe.Infrastructure.ApplicationSettings.Root
{
	public class AppSettings
	{
		public ValuesForPresentationMode ValuesForPresentationMode { get; init; }
		public Constants Constants { get; init; }
		public Databases Databases { get; init; }
		public ServiceAccounts ServiceAccounts { get; init; }
	}
}
