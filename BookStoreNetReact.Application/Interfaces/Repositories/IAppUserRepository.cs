using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Application.Dtos.UserAddress;
using BookStoreNetReact.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IAppUserRepository
    {
        IQueryable<AppUser>? GetAll(FilterAppUserDto filterDto);
        Task<AppUser?> GetByIdAsync(int userId);
        Task<AppUser?> GetByUserNameAsync(string username);
        Task<IdentityResult?> AddAsync(AppUser user, string password);
        Task<IdentityResult?> UpdateAsync(AppUser user);
        Task<IdentityResult?> RemoveAsync(AppUser user);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<UserAddress?> GetUserAddressByIdAsync(int addressId);
        void UpdateUserAddress(UserAddress address);
    }
}
