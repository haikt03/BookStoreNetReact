using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        IQueryable<Category> GetAll(FilterCategoryDto filterDto);
        Task<Category?> GetByIdAsync(int categoryId);
        IQueryable<Book> GetAllBooks(FilterBookDto filterDto, int categoryId);
    }
}
