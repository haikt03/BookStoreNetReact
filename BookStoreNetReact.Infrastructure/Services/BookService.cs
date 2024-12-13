using AutoMapper;
using BookStoreNetReact.Application.Dtos.Book;
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

        public async Task<PagedList<BookDto>?> GetAllWithFilterAsync(FilterBookDto filterDto)
        {
            try
            {
                var books = _unitOfWork.BookRepository.GetAllWithFilter(filterDto);
                var booksDto = await books.ToPagedListAsync
                (
                    selector: b => _mapper.Map<BookDto>(b),
                    pageSize: filterDto.PageSize,
                    pageIndex: filterDto.PageIndex,
                    logger: _logger
                );
                return booksDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting books");
                return null;
            }
        }

        public async Task<BookDetailDto?> GetByIdAsync(int bookId)
        {
            try
            {
                var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId, "Author");
                if (book == null)
                    throw new NullReferenceException("Book not found");
                var bookDto = _mapper.Map<BookDetailDto>(book);
                return bookDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting book by id");
                return null;
            }
        }

        public async Task<BookDetailDto?> CreateAsync(CreateBookDto createDto)
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
                    throw new InvalidOperationException("Failed to creating book");

                var bookDto = _mapper.Map<BookDetailDto>(await _unitOfWork.BookRepository.GetByIdAsync(book.Id, "Author"));
                return bookDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while creating book");
                return null;
            }
        }

        public async Task<BookDetailDto?> UpdateAsync(UpdateBookDto updateDto, int bookId)
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
                if (result)
                    return _mapper.Map<BookDetailDto>(book);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while updating book");
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int bookId)
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
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while deleting book");
                return false;
            }
        }

        public async Task<BookFilterDto?> GetFilterAsync()
        {
            try
            {
                var publishers = await _unitOfWork.BookRepository.GetStringFilterAsync(b => b.Publisher);
                var languages = await _unitOfWork.BookRepository.GetStringFilterAsync(b => b.Language);
                var categories = await _unitOfWork.BookRepository.GetStringFilterAsync(b => b.Category);
                var (minPrice, maxPrice) = await _unitOfWork.BookRepository.GetPriceRangeAsync();
                return new BookFilterDto 
                { 
                    Publishers = publishers, 
                    Languages = languages, 
                    Categories = categories, 
                    MinPrice = minPrice, 
                    MaxPrice = maxPrice
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting book filter");
                return null;
            }
        }
    }
}
