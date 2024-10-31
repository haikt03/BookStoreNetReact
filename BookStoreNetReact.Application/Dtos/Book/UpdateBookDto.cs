namespace BookStoreNetReact.Application.Dtos.Book
{
    public class UpdateBookDto : CreateBookDto
    {
        public required int Id { get; set; }
    }
}
