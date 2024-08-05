using library_system.Context;
using library_system.Entities;
using Microsoft.EntityFrameworkCore;

namespace library_system.DataAccess.Repositories
{
	public interface IBookRepository
	{
		void Add(Book book);
		Task<bool> DeleteAsync(int bookId);
		void Update(Book book);
		Task<Book?> GetByIdAsync(int id);
		Task<List<Book>> GetAllAsync();
	}

	public class BookRepository(LibraryDbContext dbContext) : IBookRepository
	{
		private readonly LibraryDbContext _dbContext = dbContext;

		public void Add(Book book)
		{
			_dbContext.Books.Add(book);
		}

		public async Task<bool> DeleteAsync(int bookId)
		{
			var book = await _dbContext.Books.FindAsync(bookId);
			if (book == null) return false;
			_dbContext.Books.Remove(book);
			return true;
		}

		public void Update(Book book)
		{
			_dbContext.Books.Update(book);
		}

		public async Task<Book?> GetByIdAsync(int id)
		{
			var book = await _dbContext.Books.FindAsync(id);
			if (book == null)
			{
				return null;
			}

			return book;
		}

		public async Task<List<Book>> GetAllAsync()
		{
			return await _dbContext.Books.ToListAsync();
		}
	}
}