using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        IQueryable<Category> GetAll(FilterCategoryDto filterCategoryDto);
        Task<Category?> GetByIdAsync(int id);
        IQueryable<Book> GetAllBooks(FilterBookDto filterBookDto, int categoryId);
    }
}
