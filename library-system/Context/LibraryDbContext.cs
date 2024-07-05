using library_system.Entities;
using Microsoft.EntityFrameworkCore;

namespace library_system.Context
{
	public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
	{
		public DbSet<Book> Books { get; set; }
		public DbSet<LibraryMember> LibraryMembers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>().HasKey(b => b.Id);
		}
	}
}
