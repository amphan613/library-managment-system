using library_system.Entities;

namespace library_system.Services
{
	public interface IDiscountStrategy
	{
		decimal CalculateDiscount(Book book);
	}

	public class AudioBookDiscountStrategy : IDiscountStrategy
	{
		public decimal CalculateDiscount(Book book)
		{
			//hardcode return here for simplicity but this method can 
			//be as complex as you want.
			//ex.  The discount rate can be retrieved via an external asynchronous API call.
			return 0.9m;
		}
	}

	public class PaperBackDiscountStrategy : IDiscountStrategy
	{
		public decimal CalculateDiscount(Book book)
		{
			//hardcode return here for simplicity but this method can 
			//be as complex as you want.
			//ex.  The discount rate can be retrieved via an external asynchronous API call.
			return 0.9m;
		}
	}

	public class DefaultDiscountStrategy : IDiscountStrategy
	{
		public decimal CalculateDiscount(Book book) => 1.0m; // No discount
	}
}
