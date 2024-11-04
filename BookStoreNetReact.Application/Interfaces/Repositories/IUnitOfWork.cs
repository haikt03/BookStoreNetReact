namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository AuthorRepository { get; }
        IBookRepository BookRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        Task<bool> CompleteAsync();
    }
}
