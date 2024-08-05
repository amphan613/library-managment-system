using library_system.Context;
using library_system.DataAccess.Repositories;

namespace library_system.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
	{
		IBookRepository Books { get; }
		Task<int> CompleteAsync();
	}

	public class UnitOfWork(LibraryDbContext dbContext, IBookRepository bookRepository) : IUnitOfWork
	{
		private readonly LibraryDbContext _dbContext = dbContext;

		public IBookRepository Books { get; private set; } = bookRepository;

		public async Task<int> CompleteAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}