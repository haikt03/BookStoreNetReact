﻿using BookStoreNetReact.Api.Extensions;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;
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
        public async Task<ActionResult<PagedList<CategoryDto>>> GetAllCategories([FromQuery] FilterCategoryDto filterCategoryDto)
        {
            var categoriesDto = await _categoryService.GetAllCategoriesAsync(filterCategoryDto);
            if (categoriesDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy thể loại" });
            Response.AddPaginationHeader(categoriesDto.Pagination);
            return Ok(categoriesDto);
        }

        [HttpGet("{id}", Name = nameof(GetCategoryById))]
        public async Task<ActionResult<DetailCategoryDto>> GetCategoryById(int id)
        {
            var detailCategoryDto = await _categoryService.GetCategoryByIdAsync(id);
            if (detailCategoryDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy thể loại" });
            return Ok(detailCategoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<DetailCategoryDto>> CreateCategory([FromForm] CreateCategoryDto createCategoryDto)
        {
            var category = await _categoryService.CreateCategoryAsync(createCategoryDto);
            if (category == null)
                return BadRequest(new ProblemDetails { Title = "Thêm mới thể loại không thành công" });
            return CreatedAtRoute(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory([FromForm] UpdateCategoryDto updateCategoryDto, int id)
        {
            var result = await _categoryService.UpdateCategoryAsync(updateCategoryDto, id);
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Cập nhật thể loại không thành công" });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Xoá thể loại không thành công" });
            return Ok();
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult<PagedList<BookDto>>> GetAllBooksByCategory([FromQuery] FilterBookDto filterBookDto, int id)
        {
            var booksDto = await _categoryService.GetAllBooksByCategoryAsync(filterBookDto, id);
            if (booksDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy sách" });
            Response.AddPaginationHeader(booksDto.Pagination);
            return Ok(booksDto);
        }
    }
}
