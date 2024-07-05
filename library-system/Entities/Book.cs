namespace library_system.Entities
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string ISBN { get; set; } = string.Empty;
		public DateTime PublishedDate { get; set; }
		public string Publisher { get; set; } = string.Empty;
		public int Pages { get; set; }
	}
}
