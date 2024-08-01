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
		BookFactoryResolver bookFactoryResolver) : IBookService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly BookFactoryResolver _bookFactoryResolver = bookFactoryResolver;

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

			// Select the appropriate discount strategy based on the book type
			IDiscountStrategy discountStrategy = book.Type switch
			{
				BookType.AudioBook => new AudioBookDiscountStrategy(),
				BookType.PaperBack => new PaperBackDiscountStrategy(),
				_ => new DefaultDiscountStrategy()
			};

			Func<Book, decimal> rentalCostCalculationFunc = b => discountStrategy.CalculateDiscount(b);

			// Apply the rental cost calculation
			book = SetRentalCost(book, rentalCostCalculationFunc);

			return book;
		}

		private Book SetRentalCost(Book book, Func<Book, decimal> rentalCostCalculationFunc)
		{
			if (book == null)
			{
				throw new ArgumentNullException(nameof(book), "Book cannot be null.");
			}

			if (rentalCostCalculationFunc == null)
			{
				throw new ArgumentNullException(nameof(rentalCostCalculationFunc), "Rental cost calculation function cannot be null.");
			}

			// Invoke the function with the book as an argument to calculate the rental price
			book.RentalPrice = rentalCostCalculationFunc(book);

			return book;
		}
	}
}