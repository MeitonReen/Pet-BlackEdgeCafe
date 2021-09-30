using System;

#nullable disable

namespace Cafe.Infrastructure.ETagCache.Databases.Model
{
	public partial class ETag
	{
		public Guid ClientId { get; set; }
		public Guid GetResourceId { get; set; }
		public string UrlGetResource { get; set; }
		public string ETagString { get; set; }
		public byte[] RowVersion { get; set; }
		public int SqliteVersion { get; set; }
	}
}
