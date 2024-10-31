using BookStoreNetReact.Application.Dtos.Image;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class CreateAuthorDto : UpsertAuthorDto
    {
        public UploadImageDto? Image { get; set; }
    }
}
