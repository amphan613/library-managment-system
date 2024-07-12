using library_system.Entities;

namespace library_system.Factories
{
    public class PaperBackBookFactory : IBookFactory
    {
        public Book AddBook(string title, string author, BookType type)
        {
            return new Book
            {
                Title = title,
                Author = author,
                Type = type,
                //For simplicity we use random, but technically this can be an API call
                //to retrieve the total page for this book.
                Pages = Random.Shared.Next(0, 1000),
            };
        }
    }
}