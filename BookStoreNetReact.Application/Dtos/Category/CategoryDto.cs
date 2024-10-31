using BookStoreNetReact.Application.Dtos.Book;

namespace BookStoreNetReact.Application.Dtos.Category
{
    public class CategoryDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public CategoryDto? PCategory { get; set; }
        public List<CategoryDto>? CCategories { get; set; }
        public List<BookDto>? Books { get; set; }
    }
}
