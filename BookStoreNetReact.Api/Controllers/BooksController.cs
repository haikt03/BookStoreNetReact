using BookStoreNetReact.Api.Extensions;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;
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
        public async Task<ActionResult<PagedList<BookDto>>> GetAllBooks([FromQuery] FilterBookDto filterBookDto)
        {
            var booksDto = await _bookService.GetAllBooksAsync(filterBookDto);
            if (booksDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy sách" });
            Response.AddPaginationHeader(booksDto.Pagination);
            return Ok(booksDto);
        }

        [HttpGet("{id}", Name = nameof(GetBookById))]
        public async Task<ActionResult<DetailBookDto>> GetBookById(int id)
        {
            var detailBookDto = await _bookService.GetBookByIdAsync(id);
            if (detailBookDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy sách" });
            return Ok(detailBookDto);
        }

        [HttpPost]
        public async Task<ActionResult<DetailBookDto>> CreateBook([FromForm] CreateBookDto createBookDto)
        {
            var book = await _bookService.CreateBookAsync(createBookDto);
            if (book == null)
                return BadRequest(new ProblemDetails { Title = "Thêm mới sách không thành công" });
            return CreatedAtRoute(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook([FromForm] UpdateBookDto updateBookDto, int id)
        {
            var result = await _bookService.UpdateBookAsync(updateBookDto, id);
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Cập nhật sách không thành công" });
            return Ok();
        }

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
