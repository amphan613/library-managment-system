using library_system.Entities;
using library_system.Services;

namespace library_system.Factories
{
	public interface IDiscountRateFactory
	{
		IDiscountStrategy GetStrategy(BookType bookType);
	}

	public class DiscountRateFactory(IServiceProvider serviceProvider)  : IDiscountRateFactory
	{
		private readonly IServiceProvider _serviceProvider = serviceProvider;

		public IDiscountStrategy GetStrategy(BookType bookType)
		{
			// Example of selecting a strategy based on the book type
			// This can be extended to use more complex logic or DI to resolve strategies
			return bookType switch
			{
				BookType.AudioBook => _serviceProvider.GetService<AudioBookDiscountStrategy>(),
				BookType.PaperBack => _serviceProvider.GetService<PaperBackDiscountStrategy>(),
				_ => _serviceProvider.GetService<DefaultDiscountStrategy>(),
			};
		}
	}
}
