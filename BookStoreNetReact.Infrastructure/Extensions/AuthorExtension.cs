using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class AuthorExtension
    {
        public static IQueryable<Author> Search(this IQueryable<Author> query, string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return query;
            }
            var lowerCaseSearch = search.Trim().ToLower();
            var result = query.Where(a => a.FullName.ToLower().Contains(lowerCaseSearch));
            return result;
        }

        public static IQueryable<Author> Filter(this IQueryable<Author> query, string? countries = null)
        {
            var countryList = new List<string>();

            if (!string.IsNullOrWhiteSpace(countries))
            {
                countryList.AddRange(countries.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            }
            var result = query.Where(a => (countryList.Count == 0 || countryList.Contains(a.Country)));
            return result;
        }

        public static IQueryable<Author> Sort(this IQueryable<Author> query, string? sort = null)
        {
            if (string.IsNullOrWhiteSpace(sort))
            {
                return query.OrderBy(a => a.FullName);
            }
            var result = sort switch
            {
                "fullName" => query.OrderBy(a => a.FullName),
                "fullNameDesc" => query.OrderByDescending(a => a.FullName),
                "country" => query.OrderBy(a => a.Country),
                "countryDesc" => query.OrderByDescending(a => a.Country),
                _ => query.OrderBy(a => a.FullName)
            };
            return result;
        }
    }
}
