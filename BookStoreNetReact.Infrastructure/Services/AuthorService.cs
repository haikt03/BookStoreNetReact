using AutoMapper;
using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using BookStoreNetReact.Application.Dtos.Book;

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
                _logger.LogWarning(ex, "An error occurred while getting all authors");
                return null;
            }
        }

        public async Task<DetailAuthorDto?> GetAuthorByIdAsync(int authorId)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepository.GetByIdAsync(authorId);
                if (author == null)
                    throw new NullReferenceException("Author not found");
                var authorDto = _mapper.Map<DetailAuthorDto>(author);
                return authorDto;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Author not found");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting author by id");
                return null;
            }
        }

        public async Task<DetailAuthorDto?> CreateAuthorAsync(CreateAuthorDto createDto)
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
                    throw new InvalidOperationException("Failed to save changes author data");

                var authorDto = _mapper.Map<DetailAuthorDto>(await _unitOfWork.AuthorRepository.GetByIdAsync(author.Id));
                return authorDto;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Failed to save changes author data");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while creating author");
                return null;
            }
        }

        public async Task<bool> UpdateAuthorAsync(UpdateAuthorDto updateDto, int authorId)
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
                return result;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Author not found");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while updating author");
                return false;
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
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Author not found");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while deleting author");
                return false;
            }
        }

        public async Task<List<string>?> GetAllCountriesOfAuthorsAsync()
        {
            try
            {
                var countries = await _unitOfWork.AuthorRepository.GetAllCountriesAsync();
                return countries;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting all countries of authors");
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
    }
}
