using BookStoreNetReact.Application.Dtos.Image;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class CreateBookDto : UpsertBookDto
    {
        public UploadImageDto? Image { get; set; }
    }
}
