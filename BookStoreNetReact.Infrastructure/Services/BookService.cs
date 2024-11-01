using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class BookService : IBookService
    {
        public Task<PagedList<BookDto>> GetAllBooks(FilterBookDto filterBookDto)
        {
            throw new NotImplementedException();
        }

        public Task<BookDto> GetBookById(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateBook(CreateBookDto createBookDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBook(UpdateBookDto updateBookDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllAuthorsOfBook()
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllCategoriesOfBook()
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllLanguagesOfBook()
        {
            throw new NotImplementedException();
        }
    }
}
