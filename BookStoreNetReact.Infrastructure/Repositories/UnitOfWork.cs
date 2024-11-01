using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Infrastructure.Data;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            AuthorRepo = new AuthorRepository(_context);
            BookRepo = new BookRepository(_context);
            CategoryRepo = new CategoryRepository(_context);
        }

        public IAuthorRepository AuthorRepo { get; private set; }
        public IBookRepository BookRepo { get; private set; }
        public ICategoryRepository CategoryRepo { get; private set; }

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
