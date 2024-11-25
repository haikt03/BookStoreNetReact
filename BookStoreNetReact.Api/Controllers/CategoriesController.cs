using BookStoreNetReact.Api.Extensions;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreNetReact.Api.Controllers
{
    [Route("api/categories")]
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<CategoryDto>>> GetAllCategories([FromQuery] FilterCategoryDto filterDto)
        {
            var categoriesDto = await _categoryService.GetAllCategoriesAsync(filterDto);
            if (categoriesDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy thể loại" });
            Response.AddPaginationHeader(categoriesDto.Pagination);
            return Ok(categoriesDto);
        }

        [HttpGet("{id}", Name = nameof(GetCategoryById))]
        public async Task<ActionResult<CategoryDetailDto>> GetCategoryById(int id)
        {
            var categoryDto = await _categoryService.GetCategoryByIdAsync(id);
            if (categoryDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy thể loại" });
            return Ok(categoryDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CategoryDetailDto>> CreateCategory([FromForm] CreateCategoryDto createDto)
        {
            var categoryDto = await _categoryService.CreateCategoryAsync(createDto);
            if (categoryDto == null)
                return BadRequest(new ProblemDetails { Title = "Thêm mới thể loại không thành công" });
            return CreatedAtRoute(nameof(GetCategoryById), new { id = categoryDto.Id }, categoryDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory([FromForm] UpdateCategoryDto updateDto, int id)
        {
            var result = await _categoryService.UpdateCategoryAsync(updateDto, id);
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Cập nhật thể loại không thành công" });
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Xoá thể loại không thành công" });
            return Ok();
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult<PagedList<BookDto>>> GetAllBooksByCategory([FromQuery] FilterBookDto filterDto, int id)
        {
            var booksDto = await _categoryService.GetAllBooksByCategoryAsync(filterDto, id);
            if (booksDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy sách" });
            Response.AddPaginationHeader(booksDto.Pagination);
            return Ok(booksDto);
        }
    }
}
