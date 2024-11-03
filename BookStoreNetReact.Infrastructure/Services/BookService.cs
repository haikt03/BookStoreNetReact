using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class BookService : IBookService
    {
        public Task<PagedList<BookDto>> GetAllBooksAsync(FilterBookDto filterBookDto)
        {
            throw new NotImplementedException();
        }

        public Task<BookDto> GetBookByIdAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBookAsync(UpdateBookDto updateBookDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBookAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllPublishersOfBooksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllLanguagesOfBooksAsync()
        {
            throw new NotImplementedException();
        }
    }
}
