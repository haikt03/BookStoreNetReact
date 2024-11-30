namespace BookStoreNetReact.Application.Dtos.Book
{
    public class BookFilterDto
    {
        public List<string>? Publishers { get; set; }
        public List<string>? Languages { get; set; }
        public List<string>? Categories { get; set; }
        public required int MinPrice { get; set; }
        public required int MaxPrice { get; set; }
    }
}
