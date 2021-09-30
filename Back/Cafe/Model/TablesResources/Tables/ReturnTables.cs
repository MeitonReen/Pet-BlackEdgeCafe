using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.TablesResources.Tables
{
	public class ReturnTables : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		public ReturnTables(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			TableDTO[] Tables = await _cafeDB.Tables
				.Select(Table => new TableDTO(Table.TableId, Table.TableNumber,
					Table.NumberOfSeats))
				.ToArrayAsync();

			request.Result = _resultGenerator.Ok(Tables);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}