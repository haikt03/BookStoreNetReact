using BookStoreNetReact.Api.Extensions;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreNetReact.Api.Controllers
{
    [Route("api/books")]
    public class BooksController : BaseApiController
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<BookDto>>> GetAllBooks([FromQuery] FilterBookDto filterDto)
        {
            var booksDto = await _bookService.GetAllBooksAsync(filterDto);
            if (booksDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy sách" });
            Response.AddPaginationHeader(booksDto.Pagination);
            return Ok(booksDto);
        }

        [HttpGet("{id}", Name = nameof(GetBookById))]
        public async Task<ActionResult<DetailBookDto>> GetBookById(int id)
        {
            var bookDto = await _bookService.GetBookByIdAsync(id);
            if (bookDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy sách" });
            return Ok(bookDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<DetailBookDto>> CreateBook([FromForm] CreateBookDto createDto)
        {
            var bookDto = await _bookService.CreateBookAsync(createDto);
            if (bookDto == null)
                return BadRequest(new ProblemDetails { Title = "Thêm mới sách không thành công" });
            return CreatedAtRoute(nameof(GetBookById), new { id = bookDto.Id }, bookDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook([FromForm] UpdateBookDto updateDto, int id)
        {
            var result = await _bookService.UpdateBookAsync(updateDto, id);
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Cập nhật sách không thành công" });
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Xoá sách không thành công" });
            return Ok();
        }

        [HttpGet("publishers")]
        public async Task<ActionResult<List<string>>> GetAllPublishersOfBooks()
        {
            var result = await _bookService.GetAllPublishersOfBooksAsync();
            if (result == null || result.Count == 0)
                return BadRequest(new ProblemDetails { Title = "Không tìm thấy các nhà xuất bản" });
            return Ok(result);
        }

        [HttpGet("languages")]
        public async Task<ActionResult<List<string>>> GetAllLanguagesOfBooks()
        {
            var result = await _bookService.GetAllLanguagesOfBooksAsync();
            if (result == null || result.Count == 0)
                return BadRequest(new ProblemDetails { Title = "Không tìm thấy các ngôn ngữ" });
            return Ok(result);
        }
    }
}
