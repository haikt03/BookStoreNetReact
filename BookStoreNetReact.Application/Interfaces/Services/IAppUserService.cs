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
        Task LogoutAsync(RefreshTokenDto logoutDto);
        Task<PagedList<AppUserDto>?> GetAllWithFilterAsync(FilterAppUserDto filterDto);
        Task<AppUserDetailDto?> GetByIdAsync(int userId);
        Task<IdentityResult?> UpdateAsync(UpdateAppUserDto updateDto, int userId);
        Task<IdentityResult?> DeleteAsync(int userId);
        Task<bool> UpdateAddressAsync(UpdateUserAddressDto updateDto, int userId);
        Task<IdentityResult?> ChangePasswordAsync(ChangePasswordDto changePasswordDto, int userId);
        Task<bool> SendEmailConfirmationAsync(int userId);
        Task<IdentityResult?> ConfirmEmailAsync(int userId, string token);
        Task<TokenDto?> RefreshAsync(RefreshTokenDto refreshDto);
        Task<bool> SendPhoneNumberConfirmationCodeAsync(int userId);
        Task<bool> ConfirmPhoneNumberAsync(ConfirmPhoneNumberDto confirmDto, int userId);
    }
}
