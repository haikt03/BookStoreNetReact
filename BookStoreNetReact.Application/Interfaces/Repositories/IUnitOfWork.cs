namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAppUserRepository AppUserRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IBookRepository BookRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        IBasketRepository BasketRepository { get; }
        Task<bool> CompleteAsync();
    }
}
