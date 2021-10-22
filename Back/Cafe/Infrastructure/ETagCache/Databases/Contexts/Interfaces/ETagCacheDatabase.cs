using Cafe.Infrastructure.ApplicationSettings.Root;
using Cafe.Infrastructure.ETagCache.Databases.Model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Cafe.Infrastructure.ETagCache.Databases.Contexts.Interfaces
{
	public class ETagCacheDatabase : DbContext
	{
		protected string _adminLogin = null;
		protected string _adminPassword = null;
		protected string _connectionString = null;
		public ETagCacheDatabase(DbContextOptions options, string adminLogin,
			string adminPassword, string connectionString)
		: base(options)
		{
			_adminLogin = adminLogin;
			_adminPassword = adminPassword;
			_connectionString = connectionString;
		}
		public ETagCacheDatabase(DbContextOptions options, AppSettings appSettings,
			string connectionString)
		: base(options)
		{
			_adminLogin = appSettings.ServiceAccounts.Admin.Login;
			_adminPassword = appSettings.ServiceAccounts.Admin.Password;
			_connectionString = connectionString;
		}
		public virtual DbSet<ETag> ETags { get; set; }
	}
}
