using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class AppUserExtension
    {
        public static IQueryable<AppUser> Search(this IQueryable<AppUser> query, string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return query;
            }
            var lowerCaseSearch = search.Trim().ToLower();
            var result = query.Where(au => au.UserName != null && au.UserName.ToLower().Contains(lowerCaseSearch));
            return result;
        }

        public static IQueryable<AppUser> Sort(this IQueryable<AppUser> query, string? sort = null)
        {
            if (string.IsNullOrWhiteSpace(sort))
            {
                return query.OrderBy(au => au.UserName);
            }
            var result = sort switch
            {
                "fullName" => query.OrderBy(au => au.FullName),
                "fullNameDesc" => query.OrderByDescending(au => au.FullName),
                "dateOfBirth" => query.OrderBy(au => au.DateOfBirth),
                "dateOfBirthDesc" => query.OrderByDescending(au => au.DateOfBirth),
                "userName" => query.OrderBy(au => au.UserName),
                "userNameDesc" => query.OrderByDescending(au => au.UserName),
                _ => query.OrderBy(au => au.UserName)
            };
            return result;
        }
    }
}
