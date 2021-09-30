using System;
using System.Collections.Generic;

#nullable disable

namespace Cafe.Databases.Cafe.Model
{
	public partial class Table
	{
		public Table()
		{
			BookedTables = new HashSet<BookedTable>();
			Carts = new HashSet<Cart>();
			Orders = new HashSet<Order>();
		}

		public Guid TableId { get; set; }
		public int TableNumber { get; set; }
		public int NumberOfSeats { get; set; }

		public virtual ICollection<BookedTable> BookedTables { get; set; }
		public virtual ICollection<Cart> Carts { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
	}
}
