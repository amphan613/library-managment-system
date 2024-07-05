using library_system.Context;

namespace library_system.Services
{
	public interface IMemberService
	{
	}

	public class MemberService(LibraryDbContext dbContext) : IMemberService
	{
		private readonly LibraryDbContext _dbContext = dbContext;
	}
}
