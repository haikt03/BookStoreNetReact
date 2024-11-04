using AutoMapper;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class BookService : GenericService<BookService>, IBookService
    {
        public BookService(IMapper mapper, ICloudUploadService cloudUploadService, IUnitOfWork unitOfWork, ILogger<BookService> logger) : base(mapper, cloudUploadService, unitOfWork, logger)
        {
        }

        public async Task<PagedList<BookDto>?> GetAllBooksAsync(FilterBookDto filterBookDto)
        {
            try
            {
                var books = _unitOfWork.BookRepository.GetAll(filterBookDto);
                if (books == null)
                    throw new NullReferenceException("Books not found");

                var result = await books.ToPagedListAsync
                (
                    selector: b => _mapper.Map<BookDto>(b),
                    pageSize: filterBookDto.PageSize,
                    pageIndex: filterBookDto.PageIndex,
                    logger: _logger
                );
                return result;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Books data not found");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting all books");
                return null;
            }
        }

        public async Task<DetailBookDto?> GetBookByIdAsync(int bookId)
        {
            try
            {
                var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
                if (book == null)
                    throw new NullReferenceException("Book not found");
                var detailBookDto = _mapper.Map<DetailBookDto>(book);
                return detailBookDto;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Book data not found");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting book by id");
                return null;
            }
        }

        public async Task<DetailBookDto?> CreateBookAsync(CreateBookDto createBookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(createBookDto);
                if (createBookDto.File != null && createBookDto.File.Length != 0)
                {
                    var imageDto = await _cloudUploadService.UploadImageAsync(createBookDto.File, folder: "Books");
                    if (imageDto != null)
                    {
                        book.PublicId = imageDto.PublicId;
                        book.ImageUrl = imageDto.ImageUrl;
                    }
                }

                await _unitOfWork.BookRepository.AddAsync(book);
                var result = await _unitOfWork.CompleteAsync();
                if (!result)
                    throw new InvalidOperationException("Failed to save changes book data");

                var newBook = await _unitOfWork.BookRepository.GetByIdAsync(book.Id);
                return _mapper.Map<DetailBookDto>(newBook);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Failed to save changes book data");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while creating book");
                return null;
            }
        }

        public async Task<bool> UpdateBookAsync(UpdateBookDto updateBookDto, int bookId)
        {
            try
            {
                var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
                if (book == null)
                    throw new NullReferenceException("Book not found");

                _mapper.Map(updateBookDto, book);
                if (updateBookDto.File != null && updateBookDto.File.Length != 0)
                {
                    if (book.PublicId != null)
                        await _cloudUploadService.DeleteImageAsync(book.PublicId);

                    var imageDto = await _cloudUploadService.UploadImageAsync(updateBookDto.File, folder: "Books");
                    if (imageDto != null)
                    {
                        book.PublicId = imageDto.PublicId;
                        book.ImageUrl = imageDto.ImageUrl;
                    }
                }

                _unitOfWork.BookRepository.Update(book);
                return await _unitOfWork.CompleteAsync();
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Book data not found");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while updating book");
                return false;
            }
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            try
            {
                var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
                if (book == null)
                    throw new NullReferenceException("Book not found");

                if (book.PublicId != null)
                    await _cloudUploadService.DeleteImageAsync(book.PublicId);

                _unitOfWork.BookRepository.Remove(book);
                return await _unitOfWork.CompleteAsync();
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Book data not found");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while deleting book");
                return false;
            }
        }

        public async Task<List<string>?> GetAllPublishersOfBooksAsync()
        {
            try
            {
                var result = await _unitOfWork.BookRepository.GetAllPublishersAsync();
                if (result == null)
                    throw new NullReferenceException("Publishers not found");
                return result;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Countries data not found");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting all publishers of authors");
                return null;
            }
        }

        public async Task<List<string>?> GetAllLanguagesOfBooksAsync()
        {
            try
            {
                var result = await _unitOfWork.BookRepository.GetAllLanguagesAsync();
                if (result == null)
                    throw new NullReferenceException("Languages not found");
                return result;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Languages data not found");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting all languages of authors");
                return null;
            }
        }
    }
}
