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
            AuthorRepository = new AuthorRepository(_context);
            BookRepository = new BookRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
        }

        public IAuthorRepository AuthorRepository { get; private set; }
        public IBookRepository BookRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }

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
