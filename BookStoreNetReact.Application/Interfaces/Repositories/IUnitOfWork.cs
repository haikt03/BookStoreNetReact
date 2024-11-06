namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAppUserRepository AppUserRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IBookRepository BookRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        Task<bool> CompleteAsync();
    }
}
