namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository AuthorRepo { get; }
        IBookRepository BookRepo { get; }
        ICategoryRepository CategoryRepo { get; }
        Task<bool> CompleteAsync();
    }
}
