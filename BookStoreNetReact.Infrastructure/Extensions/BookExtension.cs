using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class BookExtension
    {
        public static IQueryable<Book> Search(this IQueryable<Book> query, string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return query;
            }
            var lowerCaseSearch = search.Trim().ToLower();
            var result = query.Where(b => b.Name.ToLower().Contains(lowerCaseSearch));
            return result;
        }

        public static IQueryable<Book> Filter
        (
            this IQueryable<Book> query,
            string? publishers = null,
            string? languages = null,
            int? minPrice = 0,
            int? maxPrice = 0)
        {
            var publisherList = new List<string>();
            var languageList = new List<string>();

            if (!string.IsNullOrWhiteSpace(publishers))
            {
                publisherList.AddRange(publishers.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            }
            if (!string.IsNullOrWhiteSpace(languages))
            {
                languageList.AddRange(languages.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            }

            var result = query
                .Where(b => (publisherList.Count == 0 || languageList.Contains(b.Publisher)))
                .Where(b => (languageList.Count == 0 || languageList.Contains(b.Language)))
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
                "name" => query.OrderBy(b => b.Name),
                "nameDesc" => query.OrderByDescending(b => b.Name),
                "publishedYear" => query.OrderBy(b => b.PublishedYear),
                "publishedYearDesc" => query.OrderByDescending(b => b.PublishedYear),
                "price" => query.OrderBy(b => b.Price),
                "priceDesc" => query.OrderByDescending(b => b.Price),
                "priceAfterDiscount" => query.OrderBy(b => b.Price - b.Price * b.Discount / 100),
                "priceAfterDiscountDesc" => query.OrderByDescending(b => b.Price - b.Price * b.Discount / 100),
                "discount" => query.OrderBy(b => b.Discount),
                "discountDesc" => query.OrderByDescending(b => b.Discount),
                "quantityInStock" => query.OrderBy(b => b.QuantityInStock),
                "quantityInStockDesc" => query.OrderByDescending(b => b.QuantityInStock),
                _ => query.OrderBy(b => b.Name)
            };
            return result;
        }
    }
}
