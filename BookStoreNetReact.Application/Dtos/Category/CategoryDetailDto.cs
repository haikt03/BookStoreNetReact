using BookStoreNetReact.Application.Dtos.Book;

namespace BookStoreNetReact.Application.Dtos.Category
{
    public class CategoryDetailDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public CategoryDto? PCategory { get; set; }
    }
}
