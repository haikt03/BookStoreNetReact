﻿using Microsoft.AspNetCore.Http;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class CreateBookDto
    {
        public required string Name { get; set; }
        public required string Category { get; set; }
        public string? Translator { get; set; }
        public required string Publisher { get; set; }
        public required int PublishedYear { get; set; }
        public required string Language { get; set; }
        public required int Weight { get; set; }
        public required int NumberOfPages { get; set; }
        public required string Form { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }
        public required int Discount { get; set; }
        public required int QuantityInStock { get; set; }
        public required int AuthorId { get; set; }
        public IFormFile? File { get; set; }
    }
}
