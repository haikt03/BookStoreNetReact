using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class BookExtension
    {
        public static IQueryable<Book> Search(this IQueryable<Book> query, string? nameSearch, string? authorSearch)
        {
            if (string.IsNullOrWhiteSpace(nameSearch) && string.IsNullOrWhiteSpace(authorSearch))
                return query;

            var lowerCaseNameSearch = nameSearch?.Trim().ToLower();
            var lowerCaseAuthorSearch = authorSearch?.Trim().ToLower();
            var result = query
                .Where(b => (string.IsNullOrWhiteSpace(lowerCaseNameSearch) 
                    || b.Name.ToLower().Contains(lowerCaseNameSearch)))
                .Where(b => (string.IsNullOrWhiteSpace(lowerCaseAuthorSearch) || b.Author == null
                    || b.Author.FullName.ToLower().Contains(lowerCaseAuthorSearch)));
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
            var categoryList = new List<string>();

            if (!string.IsNullOrWhiteSpace(publishers))
                publisherList.AddRange(publishers.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            if (!string.IsNullOrWhiteSpace(languages))
                languageList.AddRange(languages.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            if (!string.IsNullOrWhiteSpace(categories))
                categoryList.AddRange(categories.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            var result = query
                .Where(b => (publisherList.Count == 0 || publisherList.Contains(b.Publisher)))
                .Where(b => (languageList.Count == 0 || languageList.Contains(b.Language)))
                .Where(b => (categoryList.Count == 0 || categoryList.Contains(b.Category)))
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
                "discountAsc" => query.OrderBy(b => b.Discount),
                "discountDesc" => query.OrderByDescending(b => b.Discount),
                _ => query.OrderBy(b => b.Name)
            };
            return result;
        }
    }
}
