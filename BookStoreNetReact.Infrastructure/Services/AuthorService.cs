using AutoMapper;
using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class AuthorService : GenericService<AuthorService>, IAuthorService
    {
        public AuthorService(IMapper mapper, ICloudUploadService cloudUploadService, IUnitOfWork unitOfWork, ILogger<AuthorService> logger) : base(mapper, cloudUploadService, unitOfWork, logger)
        {
        }

        public async Task<PagedList<AuthorDto>?> GetAllAuthorsAsync(FilterAuthorDto filterDto)
        {
            try
            {
                var authors = _unitOfWork.AuthorRepository.GetAll(filterDto);
                var authorsDto = await authors.ToPagedListAsync
                (
                    selector: a => _mapper.Map<AuthorDto>(a),
                    pageSize: filterDto.PageSize,
                    pageIndex: filterDto.PageIndex,
                    logger: _logger
                );
                return authorsDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting authors");
                return null;
            }
        }

        public async Task<AuthorDetailDto?> GetAuthorByIdAsync(int authorId)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepository.GetByIdAsync(authorId);
                if (author == null)
                    throw new NullReferenceException("Author not found");
                var authorDto = _mapper.Map<AuthorDetailDto>(author);
                return authorDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting author by id");
                return null;
            }
        }

        public async Task<AuthorDetailDto?> CreateAuthorAsync(CreateAuthorDto createDto)
        {
            try
            {
                var author = _mapper.Map<Author>(createDto);
                if (createDto.File != null && createDto.File.Length != 0)
                {
                    var imageDto = await _cloudUploadService.UploadImageAsync(createDto.File, folder: "Authors");
                    if (imageDto != null)
                    {
                        author.PublicId = imageDto.PublicId;
                        author.ImageUrl = imageDto.ImageUrl;
                    }
                }

                await _unitOfWork.AuthorRepository.AddAsync(author);
                var result = await _unitOfWork.CompleteAsync();
                if (!result)
                    throw new InvalidOperationException("Failed to create author");

                var authorDto = _mapper.Map<AuthorDetailDto>(await _unitOfWork.AuthorRepository.GetByIdAsync(author.Id));
                return authorDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while creating author");
                return null;
            }
        }

        public async Task<AuthorDetailDto?> UpdateAuthorAsync(UpdateAuthorDto updateDto, int authorId)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepository.GetByIdAsync(authorId);
                if (author == null)
                    throw new NullReferenceException("Author not found");

                _mapper.Map(updateDto, author);
                if (updateDto.File != null && updateDto.File.Length != 0)
                {
                    if (author.PublicId != null)
                        await _cloudUploadService.DeleteImageAsync(author.PublicId);

                    var imageDto = await _cloudUploadService.UploadImageAsync(updateDto.File, folder: "Authors");
                    if (imageDto != null)
                    {
                        author.PublicId = imageDto.PublicId;
                        author.ImageUrl = imageDto.ImageUrl;
                    }
                }

                _unitOfWork.AuthorRepository.Update(author);
                var result = await _unitOfWork.CompleteAsync();
                if (result)
                    return _mapper.Map<AuthorDetailDto>(author);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while updating author");
                return null;
            }
        }

        public async Task<bool> DeleteAuthorAsync(int authorId)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepository.GetByIdAsync(authorId);
                if (author == null)
                    throw new NullReferenceException("Author not found");

                if (author.PublicId != null)
                    await _cloudUploadService.DeleteImageAsync(author.PublicId);

                _unitOfWork.AuthorRepository.Remove(author);
                var result = await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while deleting author");
                return false;
            }
        }

        public async Task<AuthorFilterDto?> GetFilterAsync()
        {
            try
            {
                var filterDto = await _unitOfWork.AuthorRepository.GetFilterAsync();
                return filterDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting author filter");
                return null;
            }
        }

        public async Task<PagedList<BookDto>?> GetAllBooksByAuthorAsync(FilterBookDto filterDto, int authorId)
        {
            try
            {
                var books = _unitOfWork.AuthorRepository.GetAllBooks(filterDto, authorId);
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
                _logger.LogWarning(ex, "An error occurred while getting all books by authorId");
                return null;
            }
        }

        public async Task<List<AuthorForUpsertBookDto>?> GetAllAuthorsForUpsertBookAsync()
        {
            try
            {
                var authors = await _unitOfWork.AuthorRepository
                    .GetAll()
                    .Select(a => _mapper.Map<AuthorForUpsertBookDto>(a))
                    .ToListAsync();
                return authors;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting all authors for upsert book");
                return null;
            }
        }
    }
}
