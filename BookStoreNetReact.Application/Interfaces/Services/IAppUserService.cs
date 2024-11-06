using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Application.Dtos.UserAddress;
using BookStoreNetReact.Application.Helpers;
using Microsoft.AspNetCore.Identity;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IAppUserService
    {
        Task<IdentityResult?> RegisterAsync(RegisterDto registerDto);
        Task<AppUserWithTokenDto?> LoginAsync(LoginDto loginDto);
        Task<PagedList<AppUserDto>?> GetAllUsersAsync(FilterAppUserDto filterDto);
        Task<DetailAppUserDto?> GetUserByIdAsync(int userId);
        Task<IdentityResult?> UpdateUserAsync(UpdateAppUserDto updateDto, int userId);
        Task<IdentityResult?> DeleteUserAsync(int userId);
        Task<bool> UpdateUserAddressAsync(UpdateUserAddressDto updateDto, int userId);
        Task<IdentityResult?> ChangePasswordAsync(ChangePasswordDto changePasswordDto, int userId);
        Task<bool> SendEmailConfirmationAsync(int userId);
        Task<IdentityResult?> ConfirmEmailAsync(int userId, string token);
    }
}
