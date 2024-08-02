using library_system.DataAccess.UnitOfWork;
using library_system.Entities;
using library_system.Factories;

namespace library_system.Services
{
    public interface IBookService
	{
		public Task<(bool, Book)> AddAsync(Book book);
		public Task<bool> UpdateAsync(Book book);
		public Task<bool> DeleteAsync(int id);
		public Task<Book?> GetByIdAsync(int id);
	}

	public class BookService(
		IUnitOfWork unitOfWork, 
		BookFactoryResolver bookFactoryResolver,
		IDiscountRateFactory discountRateFactory) : IBookService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly BookFactoryResolver _bookFactoryResolver = bookFactoryResolver;
		private readonly IDiscountRateFactory _discountRateFactory = discountRateFactory;

		public async Task<(bool,Book)> AddAsync(Book bookToAdd)
		{
			var bookFactory = _bookFactoryResolver.Resolve(bookToAdd.Type);
			var createdBook = bookFactory.AddBook(bookToAdd.Title, bookToAdd.Author, bookToAdd.Type);

			_unitOfWork.Books.Add(createdBook);
			var result = await _unitOfWork.CompleteAsync();
			return (result > 0, createdBook);
		}

		public async Task<bool> DeleteAsync(int bookId)
		{
			var success = await _unitOfWork.Books.DeleteAsync(bookId);
			if (!success) return false;
			var result = await _unitOfWork.CompleteAsync();
			return result > 0;
		}

		public async Task<bool> UpdateAsync(Book bookToUpdate)
		{
			_unitOfWork.Books.Update(bookToUpdate);
			var result = await _unitOfWork.CompleteAsync();
			return result > 0;
		}

		public async Task<Book?> GetByIdAsync(int id)
		{
			var book = await _unitOfWork.Books.GetByIdAsync(id);

			if (book == null) return null;

			var discountStrategy = _discountRateFactory.GetStrategy(book.Type);

			book.RentalPrice = discountStrategy.CalculateDiscount(book);

			return book;
		}
	}
}