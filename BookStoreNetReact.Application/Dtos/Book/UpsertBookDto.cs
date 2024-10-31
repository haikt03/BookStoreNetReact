using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class UpsertBookDto : BaseDto
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(1, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(500, ErrorMessage = MaxLengthErrorMessage)]
        public required string Name { get; set; }
        [MinLength(1, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(100, ErrorMessage = MaxLengthErrorMessage)]
        public string? Translator { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(1, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(100, ErrorMessage = MaxLengthErrorMessage)]
        public required string Publisher { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(1, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public required int PublishedYear { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(1, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(50, ErrorMessage = MaxLengthErrorMessage)]
        public required string Language { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(1, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public required int Weight { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(1, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public required int NumberOfPages { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(1, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(50, ErrorMessage = MaxLengthErrorMessage)]
        public required string Form { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(1, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(500, ErrorMessage = MaxLengthErrorMessage)]
        public required string Description { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(1, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public required int Price { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(1, 100, ErrorMessage = RangeErrorMessage)]
        public required int Discount { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(1, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public required int QuantityInStock { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public int? CategoryId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public int? AuthorId { get; set; }
    }
}
