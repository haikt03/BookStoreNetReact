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

        public async Task<PagedList<BookDto>?> GetAllBooksAsync(FilterBookDto filterDto)
        {
            try
            {
                var books = _unitOfWork.BookRepository.GetAll(filterDto);
                if (books == null)
                    throw new NullReferenceException("Books not found");

                var booksDto = await books.ToPagedListAsync
                (
                    selector: b => _mapper.Map<BookDto>(b),
                    pageSize: filterDto.PageSize,
                    pageIndex: filterDto.PageIndex,
                    logger: _logger
                );
                return booksDto;
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
                var book = await _unitOfWork.BookRepository.GetDetailByIdAsync(bookId);
                if (book == null)
                    throw new NullReferenceException("Book not found");
                var bookDto = _mapper.Map<DetailBookDto>(book);
                return bookDto;
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

        public async Task<DetailBookDto?> CreateBookAsync(CreateBookDto createDto)
        {
            try
            {
                var book = _mapper.Map<Book>(createDto);
                if (createDto.File != null && createDto.File.Length != 0)
                {
                    var imageDto = await _cloudUploadService.UploadImageAsync(createDto.File, folder: "Books");
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

                var bookDto = _mapper.Map<DetailBookDto>(await _unitOfWork.BookRepository.GetDetailByIdAsync(book.Id));
                return bookDto;
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

        public async Task<bool> UpdateBookAsync(UpdateBookDto updateDto, int bookId)
        {
            try
            {
                var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
                if (book == null)
                    throw new NullReferenceException("Book not found");

                _mapper.Map(updateDto, book);
                if (updateDto.File != null && updateDto.File.Length != 0)
                {
                    if (book.PublicId != null)
                        await _cloudUploadService.DeleteImageAsync(book.PublicId);

                    var imageDto = await _cloudUploadService.UploadImageAsync(updateDto.File, folder: "Books");
                    if (imageDto != null)
                    {
                        book.PublicId = imageDto.PublicId;
                        book.ImageUrl = imageDto.ImageUrl;
                    }
                }

                _unitOfWork.BookRepository.Update(book);
                var result = await _unitOfWork.CompleteAsync();
                return result;
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
                var result = await _unitOfWork.CompleteAsync();
                return result;
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
                var publishers = await _unitOfWork.BookRepository.GetAllPublishersAsync();
                if (publishers == null)
                    throw new NullReferenceException("Publishers not found");
                return publishers;
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
                var languages = await _unitOfWork.BookRepository.GetAllLanguagesAsync();
                if (languages == null)
                    throw new NullReferenceException("Languages not found");
                return languages;
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
