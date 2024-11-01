using AutoMapper;
using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Application.Interfaces.Repositories;

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

        public Task<PagedList<AuthorDto>> GetAllAuthors(FilterAuthorDto filterAuthorDto)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthorDto> GetAuthorById(int authorId)
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
                Console.WriteLine("Get author by id failed");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> CreateAuthor(CreateAuthorDto createAuthorDto)
        {
            try
            {
                var author = _mapper.Map<Author>(createAuthorDto);
                if (createAuthorDto.Image != null)
                {
                    var imageDto = await _cloudUploadService.UploadImageAsync(createAuthorDto.Image, folder: "Authors");
                    author.PublicId = imageDto.PublicId;
                    author.ImageUrl = imageDto.ImageUrl;
                }
                _unitOfWork.AuthorRepo.Add(author);
                var result = await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create author failed");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> UpdateAuthor(UpdateAuthorDto updateAuthorDto)
        {
            try
            {
                var author = _unitOfWork.AuthorRepo.GetByIdAsync(updateAuthorDto.Id);
                if (author == null)
                    throw new NullReferenceException("Author not found");
                var newAuthor = _mapper.Map<Author>(updateAuthorDto);
                if (updateAuthorDto.Image != null)
                {
                    var imageDto = await _cloudUploadService.UploadImageAsync(updateAuthorDto.Image, folder: "Authors");
                    newAuthor.PublicId = imageDto.PublicId;
                    newAuthor.ImageUrl = imageDto.ImageUrl;
                }
                _unitOfWork.AuthorRepo.Update(newAuthor);
                var result = await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update author failed");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> DeleteAuthor(int authorId)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepo.GetByIdAsync(authorId);
                if (author == null)
                    throw new NullReferenceException("Author not found");
                if (author.PublicId != null)
                    await _cloudUploadService.DeleteImageAsync(author.PublicId);
                _unitOfWork.AuthorRepo.Remove(author);
                var result = await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete author failed");
                throw new Exception(ex.ToString());
            }
        }

        public Task<List<string>> GetAllCountriesOfAuthor()
        {
            throw new NotImplementedException();
        }
    }
}
