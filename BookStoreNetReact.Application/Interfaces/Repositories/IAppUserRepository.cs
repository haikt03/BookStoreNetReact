using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IAppUserRepository
    {
        IQueryable<AppUser> GetAll(FilterAppUserDto filterDto);
        Task<AppUser?> GetByIdAsync(int userId);
        Task<AppUser?> GetDetailByIdAsync(int userId);
        Task<AppUser?> GetByUserNameAsync(string username);
        Task<IList<string>> GetRolesAsync(AppUser user);
        Task<IdentityResult?> AddAsync(AppUser user, string password);
        Task<IdentityResult?> UpdateAsync(AppUser user);
        Task<IdentityResult?> RemoveAsync(AppUser user);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        void UpdateUserAddress(UserAddress address);
        Task<IdentityResult?> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword);
        Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
        Task<IdentityResult?> ConfirmEmailAsync(AppUser user, string token);
    }
}
