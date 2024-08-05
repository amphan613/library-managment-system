namespace library_system.Entities
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public int Pages { get; set; }
		public int DurationInMinutes { get; set; }
		public BookType Type { get; set; }
		public decimal RentalPrice { get; set; }
	}

	public enum BookType
	{
		PaperBack,
		AudioBook
	}
}