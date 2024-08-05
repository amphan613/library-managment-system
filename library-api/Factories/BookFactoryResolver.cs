using library_system.Entities;

namespace library_system.Factories
{
    public class BookFactoryResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public BookFactoryResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IBookFactory Resolve(BookType bookType)
        {
            var factories = _serviceProvider.GetServices<IBookFactory>();

            return bookType switch
            {
                BookType.AudioBook => factories.OfType<AudioBookFactory>().FirstOrDefault(),
                BookType.PaperBack => factories.OfType<PaperBackBookFactory>().FirstOrDefault(),
                _ => throw new KeyNotFoundException("Factory not found for the given book type.")
            };
        }
    }
}
