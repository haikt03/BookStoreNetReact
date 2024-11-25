using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class CategoryExtension
    {
        public static IQueryable<Category> Search(this IQueryable<Category> query, string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
                return query;
            var lowerCaseSearch = search.Trim().ToLower();
            var result = query.Where(c => c.Name.ToLower().Contains(lowerCaseSearch));
            return result;
        }

        public static IQueryable<Category> Filter(this IQueryable<Category> query, int? pId = 0)
        {
            if (pId == 0 || pId == null)
                return query;
            return query.Where(c => c.PId == pId);
        }

        public static IQueryable<Category> Sort(this IQueryable<Category> query, string? sort = null)
        {
            if (string.IsNullOrWhiteSpace(sort))
                return query.OrderBy(c => c.Name);
            var result = sort switch
            {
                "nameAsc" => query.OrderBy(c => c.Name),
                "nameDesc" => query.OrderByDescending(c => c.Name),
                _ => query.OrderBy(c => c.Name)
            };
            return result;
        }
    }
}
