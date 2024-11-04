using BookStoreNetReact.Api.Extensions;
using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;
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
        public async Task<ActionResult<PagedList<AuthorDto>>> GetAllAuthors([FromQuery] FilterAuthorDto filterAuthorDto)
        {
            var authorsDto = await _authorService.GetAllAuthorsAsync(filterAuthorDto);
            if (authorsDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy tác giả" });
            Response.AddPaginationHeader(authorsDto.Pagination);
            return Ok(authorsDto);
        }

        [HttpGet("{id}", Name = nameof(GetAuthorById))]
        public async Task<ActionResult<DetailAuthorDto>> GetAuthorById(int id)
        {
            var detailAuthorDto = await _authorService.GetAuthorByIdAsync(id);
            if (detailAuthorDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy tác giả" });
            return Ok(detailAuthorDto);
        }

        [HttpPost]
        public async Task<ActionResult<DetailAuthorDto>> CreateAuthor([FromForm] CreateAuthorDto createAuthorDto)
        {
            var author = await _authorService.CreateAuthorAsync(createAuthorDto);
            if (author == null)
                return BadRequest(new ProblemDetails { Title = "Thêm mới tác giả không thành công" });
            return CreatedAtRoute(nameof(GetAuthorById), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor([FromForm] UpdateAuthorDto updateAuthorDto, int id)
        {
            var result = await _authorService.UpdateAuthorAsync(updateAuthorDto, id);
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Cập nhật tác giả không thành công" });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthorAsync(id);
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Xoá tác giả không thành công" });
            return Ok();
        }

        [HttpGet("countries")]
        public async Task<ActionResult<List<string>>> GetAllCountriesOfAuthors()
        {
            var result = await _authorService.GetAllCountriesOfAuthorsAsync();
            if (result == null || result.Count == 0)
                return BadRequest(new ProblemDetails { Title = "Không tìm thấy nơi sinh của các tác giả " });
            return Ok(result);
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult<PagedList<BookDto>>> GetAllBooksByAuthor([FromQuery] FilterBookDto filterBookDto, int id)
        {
            var booksDto = await _authorService.GetAllBooksByAuthorAsync(filterBookDto, id);
            if (booksDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy sách" });
            Response.AddPaginationHeader(booksDto.Pagination);
            return Ok(booksDto);
        }
    }
}