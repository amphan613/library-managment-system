using library_system.Entities;

namespace library_system.DataAccess.Factories
{
	public interface IBookFactory
	{
		Book AddBook(string title, string author, BookType type);
	}
}