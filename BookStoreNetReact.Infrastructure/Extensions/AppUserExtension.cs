using BookStoreNetReact.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class AppUserExtension
    {
        public static IQueryable<AppUser> Search(this IQueryable<AppUser> query, string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
                return query;
            var lowerCaseSearch = search.Trim().ToLower();
            var result = query.Where(au => au.UserName != null && au.UserName.ToLower().Contains(lowerCaseSearch));
            return result;
        }

        public static IQueryable<AppUser> Sort(this IQueryable<AppUser> query, string? sort = null)
        {
            if (string.IsNullOrWhiteSpace(sort))
                return query.OrderBy(au => au.UserName);
            var result = sort switch
            {
                "fullNameAsc" => query.OrderBy(au => au.FullName),
                "fullNameDesc" => query.OrderByDescending(au => au.FullName),
                "dateOfBirthAsc" => query.OrderBy(au => au.DateOfBirth),
                "dateOfBirthDesc" => query.OrderByDescending(au => au.DateOfBirth),
                "userNameAsc" => query.OrderBy(au => au.UserName),
                "userNameDesc" => query.OrderByDescending(au => au.UserName),
                _ => query.OrderBy(au => au.UserName)
            };
            return result;
        }
    }
}
