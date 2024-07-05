namespace library_system.Entities
{
	public class LibraryMember
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public DateTime MembershipDate { get; set; }

		// Constructor
		public LibraryMember()
		{
			MembershipDate = DateTime.Now; // Sets the membership date to the current date by default
		}
	}
}
