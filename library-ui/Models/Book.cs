namespace library_ui.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public int Pages { get; set; }
		public int DurationInMinutes { get; set; }
		public string Type { get; set; }
		public decimal RentalPrice { get; set; } 
	}
}
