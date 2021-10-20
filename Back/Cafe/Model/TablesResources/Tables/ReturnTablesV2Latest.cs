using Cafe.Databases.Cafe.Context.Interfaces;
using Cafe.Infrastructure.HandlersChain;
using Cafe.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Model.TablesResources.Tables
{
	public class ReturnTablesV2Latest : HandlerBase
	{
		private readonly CafeDatabase _cafeDB = null;

		public ReturnTablesV2Latest(CafeDatabase cafeDB)
		{
			_cafeDB = cafeDB;
		}
		protected override async Task ExecuteAsync(ChainRequest request)
		{
			TableDTO[] Tables = await (
				from table in _cafeDB.Tables
				select new TableDTO(table.TableId, table.TableNumber, table.NumberOfSeats))
				.ToArrayAsync();

			request.Result = _resultGenerator.Ok(Tables);
			request.Status = ChainProcessingStatus.Success;
			return;
		}
	}
}