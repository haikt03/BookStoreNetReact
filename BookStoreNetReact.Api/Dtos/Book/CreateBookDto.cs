using BookStoreNetReact.Application.Dtos.Book;
using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Api.Dtos.Book
{
    public class CreateBookDto : UpsertBookDto
    {
        [FileExtensions(Extensions = ".jpg,.jpeg,.png")]
        public IFormFile? file { get; set; }
    }
}
