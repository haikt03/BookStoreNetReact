using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Author> GetAllWithFilter(FilterAuthorDto filterDto)
        {
            var authors = _context.Authors
                .Search(filterDto.FullNameSearch)
                .Filter(filterDto.Countries)
                .Sort(filterDto.Sort);
            return authors;
        }

        public IQueryable<Author> GetAll()
        {
            var authors = _context.Authors;
            return authors;
        }

        public async Task<AuthorFilterDto> GetFilterAsync()
        {
            var contries = await _context.Authors
                .Select(a => a.Country)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .ToListAsync();
            return new AuthorFilterDto { Countries = contries };
        }
    }
}
