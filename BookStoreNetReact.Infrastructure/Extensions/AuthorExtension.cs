using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class AuthorExtension
    {
        public static IQueryable<Author> Search(this IQueryable<Author> query, string? fullNameSearch = null)
        {
            if (string.IsNullOrWhiteSpace(fullNameSearch))
                return query;
            var lowerCaseFullNameSearch = fullNameSearch.Trim().ToLower();
            var result = query.Where(a => a.FullName.ToLower().Contains(lowerCaseFullNameSearch));
            return result;
        }

        public static IQueryable<Author> Filter(this IQueryable<Author> query, string? countries = null)
        {
            var countryList = new List<string>();
            if (!string.IsNullOrWhiteSpace(countries))
                countryList.AddRange(countries.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            var result = query.Where(a => (countryList.Count == 0 || countryList.Contains(a.Country)));
            return result;
        }

        public static IQueryable<Author> Sort(this IQueryable<Author> query, string? sort = null)
        {
            if (string.IsNullOrWhiteSpace(sort))
                return query.OrderBy(a => a.FullName);
            var result = sort switch
            {
                "fullNameAsc" => query.OrderBy(a => a.FullName),
                "fullNameDesc" => query.OrderByDescending(a => a.FullName),
                _ => query.OrderBy(a => a.FullName)
            };
            return result;
        }
    }
}
