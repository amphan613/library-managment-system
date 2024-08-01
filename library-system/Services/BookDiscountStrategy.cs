using library_system.Entities;

namespace library_system.Services
{
	public interface IDiscountStrategy
	{
		decimal CalculateDiscount(Book book);
	}

	public class AudioBookDiscountStrategy : IDiscountStrategy
	{
		public decimal CalculateDiscount(Book book) => 0.9m; // 10% discount
	}

	public class PaperBackDiscountStrategy : IDiscountStrategy
	{
		public decimal CalculateDiscount(Book book) => 0.8m; // 20% discount
	}

	public class DefaultDiscountStrategy : IDiscountStrategy
	{
		public decimal CalculateDiscount(Book book) => 1.0m; // No discount
	}
}
