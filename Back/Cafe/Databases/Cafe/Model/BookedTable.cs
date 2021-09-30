using System;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class BookedTable
	{
		public Guid BookedTableId { get; set; }
		public Guid ClientId { get; set; }
		public Guid TableId { get; set; }
		public DateTime DateTimeATableIsWillBeFree { get; set; }
		public virtual Table Table { get; set; }
	}
}
