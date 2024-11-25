using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UnitOfWork(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            AuthorRepository = new AuthorRepository(_context);
            BookRepository = new BookRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            RefreshTokenRepository = new RefreshTokenRepository(_context);
            AppUserRepository = new AppUserRepository(_userManager, _context);
            BasketRepository = new BasketRepository(_context);
        }

        public IAppUserRepository AppUserRepository { get; private set; }
        public IAuthorRepository AuthorRepository { get; private set; }
        public IBookRepository BookRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; private set; }
        public IBasketRepository BasketRepository { get; private set; }

        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
