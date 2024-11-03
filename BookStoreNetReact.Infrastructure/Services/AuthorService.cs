using AutoMapper;
using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Infrastructure.Extensions;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly ICloudUploadService _cloudUploadService;
        private readonly IUnitOfWork _unitOfWork;
        public AuthorService(IMapper mapper, ICloudUploadService cloudUploadService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _cloudUploadService = cloudUploadService;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<AuthorDto>> GetAllAuthorsAsync(FilterAuthorDto filterAuthorDto)
        {
            try
            {
                var authors = _unitOfWork.AuthorRepo.GetAllAsync(filterAuthorDto);
                var result = await authors.ToPagedListAsync
                (
                    selector: a => _mapper.Map<AuthorDto>(a),
                    pageSize: filterAuthorDto.PageSize,
                    pageIndex: filterAuthorDto.PageIndex
                );
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetAllAuthorsAsync)} failed");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int authorId)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepo.GetByIdAsync(authorId);
                if (author == null)
                    throw new NullReferenceException("Author not found");
                var authorDto = _mapper.Map<AuthorDto>(author);
                return authorDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetAuthorByIdAsync)} failed");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto)
        {
            try
            {
                var author = _mapper.Map<Author>(createAuthorDto);
                if (createAuthorDto.File != null && createAuthorDto.File.Length != 0)
                {
                    var imageDto = await _cloudUploadService.UploadImageAsync(createAuthorDto.File, folder: "Authors");
                    author.PublicId = imageDto.PublicId;
                    author.ImageUrl = imageDto.ImageUrl;
                }

                await _unitOfWork.AuthorRepo.AddAsync(author);
                var result = await _unitOfWork.CompleteAsync();
                if (!result)
                    throw new Exception($"{CreateAuthorAsync} failed");
                return _mapper.Map<AuthorDto>(author);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(CreateAuthorAsync)} failed");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> UpdateAuthorAsync(UpdateAuthorDto updateAuthorDto, int authorId)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepo.GetByIdAsync(authorId);
                if (author == null)
                    throw new NullReferenceException("Author not found");

                _mapper.Map(updateAuthorDto, author);
                if (updateAuthorDto.File != null && updateAuthorDto.File.Length != 0)
                {
                    if (author.PublicId != null)
                        await _cloudUploadService.DeleteImageAsync(author.PublicId);

                    var imageDto = await _cloudUploadService.UploadImageAsync(updateAuthorDto.File, folder: "Authors");
                    author.PublicId = imageDto.PublicId;
                    author.ImageUrl = imageDto.ImageUrl;
                }

                _unitOfWork.AuthorRepo.Update(author);
                return await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(UpdateAuthorAsync)} failed");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> DeleteAuthorAsync(int authorId)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepo.GetByIdAsync(authorId);
                if (author == null)
                    throw new NullReferenceException("Author not found");

                if (author.PublicId != null)
                    await _cloudUploadService.DeleteImageAsync(author.PublicId);

                _unitOfWork.AuthorRepo.Remove(author);
                return await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(DeleteAuthorAsync)} failed");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<string>> GetAllCountriesOfAuthorsAsync()
        {
            return await _unitOfWork.AuthorRepo.GetAllCountriesAsync();
        }
    }
}
