using AutoMapper;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
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

        public async Task<BookDetailDto?> GetBookByIdAsync(int bookId)
        {
            try
            {
                var book = await _unitOfWork.BookRepository.GetDetailByIdAsync(bookId);
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

        public async Task<BookDetailDto?> CreateBookAsync(CreateBookDto createDto)
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

                var bookDto = _mapper.Map<BookDetailDto>(await _unitOfWork.BookRepository.GetDetailByIdAsync(book.Id));
                return bookDto;
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
                var filterDto = await _unitOfWork.BookRepository.GetFilterAsync();
                var categoriesDto = await _unitOfWork.CategoryRepository
                    .GetAll()
                    .Select(c => _mapper.Map<CategoryDto>(c))
                    .ToListAsync();
                filterDto.Categories = categoriesDto;
                return filterDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting book filter");
                return null;
            }
        }
    }
}
