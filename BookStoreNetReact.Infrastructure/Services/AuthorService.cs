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

        public async Task<PagedList<AuthorDto>?> GetAllAuthorsAsync(FilterAuthorDto filterAuthorDto)
        {
            try
            {
                var authors = _unitOfWork.AuthorRepository.GetAll(filterAuthorDto);
                if (authors == null)
                    throw new NullReferenceException("Authors not found");

                var result = await authors.ToPagedListAsync
                (
                    selector: a => _mapper.Map<AuthorDto>(a),
                    pageSize: filterAuthorDto.PageSize,
                    pageIndex: filterAuthorDto.PageIndex,
                    logger: _logger
                );
                return result;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Authors data not found");
                return null;
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
                var detailAthorDto = _mapper.Map<DetailAuthorDto>(author);
                return detailAthorDto;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Author data not found");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting author by id");
                return null;
            }
        }

        public async Task<DetailAuthorDto?> CreateAuthorAsync(CreateAuthorDto createAuthorDto)
        {
            try
            {
                var author = _mapper.Map<Author>(createAuthorDto);
                if (createAuthorDto.File != null && createAuthorDto.File.Length != 0)
                {
                    var imageDto = await _cloudUploadService.UploadImageAsync(createAuthorDto.File, folder: "Authors");
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

                var newAuthor = await _unitOfWork.AuthorRepository.GetByIdAsync(author.Id);
                return _mapper.Map<DetailAuthorDto>(newAuthor);
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

        public async Task<bool> UpdateAuthorAsync(UpdateAuthorDto updateAuthorDto, int authorId)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepository.GetByIdAsync(authorId);
                if (author == null)
                    throw new NullReferenceException("Author not found");

                _mapper.Map(updateAuthorDto, author);
                if (updateAuthorDto.File != null && updateAuthorDto.File.Length != 0)
                {
                    if (author.PublicId != null)
                        await _cloudUploadService.DeleteImageAsync(author.PublicId);

                    var imageDto = await _cloudUploadService.UploadImageAsync(updateAuthorDto.File, folder: "Authors");
                    if (imageDto != null)
                    {
                        author.PublicId = imageDto.PublicId;
                        author.ImageUrl = imageDto.ImageUrl;
                    }
                }

                _unitOfWork.AuthorRepository.Update(author);
                return await _unitOfWork.CompleteAsync();
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Author data not found");
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
                return await _unitOfWork.CompleteAsync();
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Author data not found");
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
                var result = await _unitOfWork.AuthorRepository.GetAllCountriesAsync();
                if (result == null)
                    throw new NullReferenceException("Countries not found");
                return result;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Countries data not found");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting all countries of authors");
                return null;
            }
        }

        public async Task<PagedList<BookDto>?> GetAllBooksByAuthorAsync(FilterBookDto filterBookDto, int authorId)
        {
            try
            {
                var books = _unitOfWork.AuthorRepository.GetAllBooks(filterBookDto, authorId);
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
                _logger.LogWarning(ex, "An error occurred while getting all books by authorId");
                return null;
            }
        }
    }
}
