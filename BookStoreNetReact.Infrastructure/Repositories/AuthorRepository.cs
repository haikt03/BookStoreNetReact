using BookStoreNetReact.Application.Dtos.Author;
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

        public IQueryable<Author> GetAllAsync(FilterAuthorDto filterAuthorDto)
        {
            var authors = _context.Authors
                .Search(filterAuthorDto.Search)
                .Filter(filterAuthorDto.Countries)
                .Sort(filterAuthorDto.Sort);
            return authors;
        }

        public async Task<List<string>> GetAllCountriesAsync()
        {
            return await _context.Authors
                .Select(a => a.Country)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .ToListAsync();
        }
    }
}
