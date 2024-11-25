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
    public class CategoryService : GenericService<CategoryService>, ICategoryService
    {
        public CategoryService(IMapper mapper, ICloudUploadService cloudUploadService, IUnitOfWork unitOfWork, ILogger<CategoryService> logger) : base(mapper, cloudUploadService, unitOfWork, logger)
        {
        }

        public async Task<PagedList<CategoryDto>?> GetAllCategoriesAsync(FilterCategoryDto filterCategoryDto)
        {
            try
            {
                var categories = _unitOfWork.CategoryRepository.GetAll(filterCategoryDto);
                var result = await categories.ToPagedListAsync
                (
                    selector: c => _mapper.Map<CategoryDto>(c),
                    pageSize: filterCategoryDto.PageSize,
                    pageIndex: filterCategoryDto.PageIndex,
                    logger: _logger
                );
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting categories");
                return null;
            }
        }

        public async Task<CategoryDetailDto?> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetDetailByIdAsync(categoryId);
                if (category == null)
                    throw new NullReferenceException("Category not found");
                var categoryDto = _mapper.Map<CategoryDetailDto>(category);
                return categoryDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting category by id");
                return null;
            }
        }

        public async Task<CategoryDetailDto?> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(createCategoryDto);
                await _unitOfWork.CategoryRepository.AddAsync(category);
                var result = await _unitOfWork.CompleteAsync();

                if (!result)
                    throw new InvalidOperationException("Failed to creating category");

                var categoryDto = _mapper.Map<CategoryDetailDto>(await _unitOfWork.CategoryRepository.GetDetailByIdAsync(category.Id));
                return categoryDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while creating category");
                return null;
            }
        }

        public async Task<bool> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, int categoryId)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                    throw new NullReferenceException("Category not found");

                _mapper.Map(updateCategoryDto, category);
                _unitOfWork.CategoryRepository.Update(category);
                return await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while updating category");
                return false;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                    throw new NullReferenceException("Category not found");

                _unitOfWork.CategoryRepository.Remove(category);
                return await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while deleting category");
                return false;
            }
        }

        public async Task<PagedList<BookDto>?> GetAllBooksByCategoryAsync(FilterBookDto filterBookDto, int categoryId)
        {
            try
            {
                var books = _unitOfWork.CategoryRepository.GetAllBooks(filterBookDto, categoryId);
                var result = await books.ToPagedListAsync
                (
                    selector: b => _mapper.Map<BookDto>(b),
                    pageSize: filterBookDto.PageSize,
                    pageIndex: filterBookDto.PageIndex,
                    logger: _logger
                );
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting all books by categoryId");
                return null;
            }
        }
    }
}
