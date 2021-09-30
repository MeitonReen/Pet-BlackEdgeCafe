using Cafe.Infrastructure.ETagCache.Databases.Model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Cafe.Infrastructure.ETagCache.Databases.Contexts.Interfaces
{
	public class ETagCacheDatabase : DbContext
	{
		public ETagCacheDatabase(DbContextOptions options)
			: base(options)
		{
		}
		public virtual DbSet<ETag> ETags { get; set; }
	}
}
