using BookStoreNetReact.Application.Dtos.Author;
using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Api.Dtos.Author
{
    public class CreateAuthorDto : UpsertAuthorDto
    {
        [FileExtensions(Extensions = ".jpg,.jpeg,.png")]
        public IFormFile? file { get; set; }
    }
}
