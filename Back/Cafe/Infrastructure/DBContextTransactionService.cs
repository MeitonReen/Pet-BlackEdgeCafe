
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Cafe.Infrastructure
{
	public class DBContextTransactionService
	{
		public Action DecorateTransaction(DbContext dbContext,
			IsolationLevel isolationLevel, Action editDB)
		{
			return new Action(() =>
			{
				using IDbContextTransaction transaction =
					dbContext.Database.BeginTransaction(isolationLevel);
				try
				{
					editDB.Invoke();
					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw new Exception();
				}
			});
		}
		public Func<Task> DecorateTransactionAsync(DbContext dbContext,
			IsolationLevel isolationLevel, Func<Task> editDBAsync)
		{
			return new Func<Task>(async () =>
			{
				using IDbContextTransaction transaction =
					await dbContext.Database.BeginTransactionAsync(isolationLevel);
				try
				{
					await editDBAsync.Invoke();
					await transaction.CommitAsync();
				}
				catch (Exception)
				{
					await transaction.RollbackAsync();
					throw new Exception("Rollback transaction");
				}
			});
		}
		public Action DecorateTransaction(DbContext dbContext, Action editDB)
		{
			return DecorateTransaction(dbContext, IsolationLevel.Unspecified, editDB);
		}
		public Func<Task> DecorateTransactionAsync(DbContext dbContext, Func<Task> editDBAsync)
		{
			return DecorateTransactionAsync(dbContext, IsolationLevel.Unspecified, editDBAsync);
		}
	}
}
