﻿using BookStoreNetReact.Api.Extensions;
using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreNetReact.Api.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : BaseApiController
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<AuthorDto>>> GetAllAuthors([FromQuery] FilterAuthorDto filterDto)
        {
            var authorsDto = await _authorService.GetAllWithFilterAsync(filterDto);
            if (authorsDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy tác giả" });
            Response.AddPaginationHeader(authorsDto.Pagination);
            return Ok(authorsDto);
        }

        [HttpGet("{id}", Name = nameof(GetAuthorById))]
        public async Task<ActionResult<AuthorDetailDto>> GetAuthorById(int id)
        {
            var authorDto = await _authorService.GetByIdAsync(id);
            if (authorDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy tác giả" });
            return Ok(authorDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AuthorDetailDto>> CreateAuthor([FromForm] CreateAuthorDto createDto)
        {
            var authorDto = await _authorService.CreateAsync(createDto);
            if (authorDto == null)
                return BadRequest(new ProblemDetails { Title = "Thêm mới tác giả không thành công" });
            return CreatedAtRoute(nameof(GetAuthorById), new { id = authorDto.Id }, authorDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDetailDto>> UpdateAuthor([FromForm] UpdateAuthorDto updateDto, int id)
        {
            var authorDto = await _authorService.UpdateAsync(updateDto, id);
            if (authorDto == null)
                return BadRequest(new ProblemDetails { Title = "Cập nhật tác giả không thành công" });
            return Ok(authorDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAsync(id);
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Xoá tác giả không thành công" });
            return Ok();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<AuthorFilterDto>> GetFilter()
        {
            var filterDto = await _authorService.GetFilterAsync();
            if (filterDto == null)
                return BadRequest(new ProblemDetails { Title = "Không tìm thấy bộ lọc tác giả " });
            return Ok(filterDto);
        }

        [HttpGet("upsert-book")]
        public async Task<ActionResult<List<AuthorForUpsertBookDto>>> GetAuthorsForUpsertBook()
        {
            var authorsDto = await _authorService.GetAllForUpsertBookAsync();
            if (authorsDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy tác giả" });
            return Ok(authorsDto);
        }
    }
}