using library_system.DataAccess.UnitOfWork;
using library_system.Entities;

namespace library_system.Services
{
	public interface IBookService
	{
		public Task<bool> AddAsync(Book book);
		public Task<bool> UpdateAsync(Book book);
		public Task<bool> DeleteAsync(int id);
		public Task<Book?> GetByIdAsync(int id);
	}

	public class BookService(IUnitOfWork unitOfWork) : IBookService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		public async Task<bool> AddAsync(Book book)
		{
			_unitOfWork.Books.Add(book);
			var result = await _unitOfWork.CompleteAsync();
			return result > 0;
		}

		public async Task<bool> DeleteAsync(int bookId)
		{
			var success = await _unitOfWork.Books.DeleteAsync(bookId);
			if (!success) return false;
			var result = await _unitOfWork.CompleteAsync();
			return result > 0;
		}

		public async Task<bool> UpdateAsync(Book book)
		{
			_unitOfWork.Books.Update(book);
			var result = await _unitOfWork.CompleteAsync();
			return result > 0;
		}

		public async Task<Book?> GetByIdAsync(int id)
		{
			var book = await _unitOfWork.Books.GetByIdAsync(id);
			if (book == null)
			{
				return null;
			}

			return book;
		}
	}
}
