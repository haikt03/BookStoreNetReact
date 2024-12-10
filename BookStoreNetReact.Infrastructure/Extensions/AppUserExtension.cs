using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class AppUserExtension
    {
        public static IQueryable<AppUser> Search
        (
            this IQueryable<AppUser> query, 
            string? fullNameSearch, 
            string? emailSearch, 
            string? phoneNumberSearch
        )
        {
            if (
                string.IsNullOrWhiteSpace(fullNameSearch) 
                && string.IsNullOrWhiteSpace(emailSearch) 
                && string.IsNullOrWhiteSpace(phoneNumberSearch)
            )
                return query;

            var lowerCaseFullNameSearch = fullNameSearch?.Trim().ToLower();
            var lowerCaseEmailSearch = emailSearch?.Trim().ToLower();
            var lowerCasePhoneNumberSearch = phoneNumberSearch?.Trim().ToLower();

            var result = query
                .Where(au => string.IsNullOrWhiteSpace(lowerCaseFullNameSearch)
                    || au.FullName.ToLower().Contains(lowerCaseFullNameSearch))
                .Where(au => string.IsNullOrWhiteSpace(lowerCaseEmailSearch) || au.Email == null 
                    || au.Email.ToLower().Contains(lowerCaseEmailSearch))
                .Where(au => string.IsNullOrWhiteSpace(lowerCasePhoneNumberSearch) || au.PhoneNumber == null 
                    || au.PhoneNumber.ToLower().Contains(lowerCasePhoneNumberSearch));
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
                _ => query.OrderBy(au => au.UserName)
            };
            return result;
        }
    }
}
