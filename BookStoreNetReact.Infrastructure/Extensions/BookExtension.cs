using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class BookExtension
    {
        public static IQueryable<Book> Search(this IQueryable<Book> query, string? search = null, string? authorSearch = null)
        {
            if (string.IsNullOrWhiteSpace(search) && string.IsNullOrWhiteSpace(authorSearch))
                return query;

            var lowerCaseSearch = search?.Trim().ToLower();
            var lowerCaseAuthorSearch = authorSearch?.Trim().ToLower();
            var result = query.Where
            (
                b => (string.IsNullOrEmpty(lowerCaseSearch) || b.Name.ToLower().Contains(lowerCaseSearch))
                    && (b.Author == null
                        || string.IsNullOrEmpty(lowerCaseAuthorSearch)
                        || b.Author.FullName.ToLower().Contains(lowerCaseAuthorSearch))
            );
            return result;
        }

        public static IQueryable<Book> Filter
        (
            this IQueryable<Book> query,
            string? publishers = null,
            string? languages = null,
            string? categories = null,
            int? minPrice = 0,
            int? maxPrice = 0)
        {
            var publisherList = new List<string>();
            var languageList = new List<string>();
            var categoryList = new List<int>();

            if (!string.IsNullOrWhiteSpace(publishers))
                publisherList.AddRange(publishers.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            if (!string.IsNullOrWhiteSpace(languages))
                languageList.AddRange(languages.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            if (!string.IsNullOrWhiteSpace(categories))
                categoryList.AddRange(categories.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());

            var result = query
                .Where(b => (publisherList.Count == 0 || publisherList.Contains(b.Publisher)))
                .Where(b => (languageList.Count == 0 || languageList.Contains(b.Language)))
                .Where(b => (categoryList.Count == 0 || b.Category == null || categoryList.Contains(b.Category.Id)))
                .Where(b => (minPrice == 0 || b.Price >= minPrice))
                .Where(b => (maxPrice == 0 || b.Price <= maxPrice));
            return result;
        }

        public static IQueryable<Book> Sort(this IQueryable<Book> query, string? sort = null)
        {
            if (string.IsNullOrWhiteSpace(sort))
            {
                return query.OrderBy(b => b.Name);
            }
            var result = sort switch
            {
                "nameAsc" => query.OrderBy(b => b.Name),
                "nameDesc" => query.OrderByDescending(b => b.Name),
                "publishedYearAsc" => query.OrderBy(b => b.PublishedYear),
                "publishedYearDesc" => query.OrderByDescending(b => b.PublishedYear),
                "priceAsc" => query.OrderBy(b => b.Price),
                "priceDesc" => query.OrderByDescending(b => b.Price),
                "priceAfterDiscountAsc" => query.OrderBy(b => b.Price - b.Price * b.Discount / 100),
                "priceAfterDiscountDesc" => query.OrderByDescending(b => b.Price - b.Price * b.Discount / 100),
                "discountAsc" => query.OrderBy(b => b.Discount),
                "discountDesc" => query.OrderByDescending(b => b.Discount),
                "quantityInStockAsc" => query.OrderBy(b => b.QuantityInStock),
                "quantityInStockDesc" => query.OrderByDescending(b => b.QuantityInStock),
                _ => query.OrderBy(b => b.Name)
            };
            return result;
        }
    }
}
