using AutoMapper;
using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Application.Dtos.UserAddress;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class AppUserService : GenericService<AppUserService>, IAppUserService
    {
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        public AppUserService
        (
            IMapper mapper,
            ICloudUploadService cloudUploadService,
            IUnitOfWork unitOfWork, ILogger<AppUserService> logger,
            ITokenService tokenService,
            IEmailService emailService,
            ISmsService smsService
        ) : base(mapper, cloudUploadService, unitOfWork, logger)
        {
            _tokenService = tokenService;
            _emailService = emailService;
            _smsService = smsService;
        }

        public async Task<IdentityResult?> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var user = _mapper.Map<AppUser>(registerDto);
                var identityResult = await _unitOfWork.AppUserRepository.AddAsync(user, registerDto.Password);

                var basket = new Basket { UserId = user.Id };
                await _unitOfWork.BasketRepository.AddAsync(basket);
                var result = await _unitOfWork.CompleteAsync();
                if (!result)
                    throw new InvalidOperationException("Failed to initialize basket");

                return identityResult;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while signing up");
                return null;
            }
        }

        public async Task<TokenDto?> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _unitOfWork.AppUserRepository.GetByUserNameAsync(loginDto.UserName);
                if (user == null)
                    throw new NullReferenceException("User not found");
                if (!await _unitOfWork.AppUserRepository.CheckPasswordAsync(user, loginDto.Password))
                    throw new Exception("Wrong username or password");

                var accessToken = await _tokenService.GenerateAccessToken(user);
                var refreshToken = await _tokenService.GenerateRefreshToken(user);
                if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
                    return null;
                var tokenDto = new TokenDto { AccessToken = accessToken, RefreshToken = refreshToken };
                return tokenDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while sigining in");
                return null;
            }
        }

        public async Task<PagedList<AppUserDto>?> GetAllUsersAsync(FilterAppUserDto filterDto)
        {
            try
            {
                var users = _unitOfWork.AppUserRepository.GetAll(filterDto);
                var result = await users.ToPagedListAsync
                    (
                        selector: au => _mapper.Map<AppUserDto>(au),
                        pageSize: filterDto.PageSize,
                        pageIndex: filterDto.PageIndex,
                        logger: _logger
                    );
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting users");
                return null;
            }
        }

        public async Task<AppUserDetailDto?> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.AppUserRepository.GetDetailByIdAsync(userId);
                if (user == null)
                    throw new NullReferenceException("User not found");
                var userDto = _mapper.Map<AppUserDetailDto>(user);
                return userDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting user by id");
                return null;
            }
        }

        public async Task<IdentityResult?> UpdateUserAsync(UpdateAppUserDto updateDto, int userId)
        {
            try
            {
                var user = await _unitOfWork.AppUserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new NullReferenceException("User not found");

                _mapper.Map(updateDto, user);
                if (updateDto.File != null && updateDto.File.Length != 0)
                {
                    if (user.PublicId != null)
                        await _cloudUploadService.DeleteImageAsync(user.PublicId);

                    var imageDto = await _cloudUploadService.UploadImageAsync(updateDto.File, folder: "Users");
                    if (imageDto != null)
                    {
                        user.PublicId = imageDto.PublicId;
                        user.ImageUrl = imageDto.ImageUrl;
                    }
                }

                var result = await _unitOfWork.AppUserRepository.UpdateAsync(user);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while updating user");
                return null;
            }
        }

        public async Task<IdentityResult?> DeleteUserAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.AppUserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new NullReferenceException("User not found");

                if (user.PublicId != null)
                    await _cloudUploadService.DeleteImageAsync(user.PublicId);

                var result = await _unitOfWork.AppUserRepository.RemoveAsync(user);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while deleting user");
                return null;
            }
        }

        public async Task<bool> UpdateUserAddressAsync(UpdateUserAddressDto updateDto, int userId)
        {
            try
            {
                var user = await _unitOfWork.AppUserRepository.GetDetailByIdAsync(userId);
                if (user == null || user.Address == null)
                    throw new NullReferenceException("Address not found");

                var address = user.Address;
                _mapper.Map(updateDto, address);
                _unitOfWork.AppUserRepository.UpdateUserAddress(address);

                var result = await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while updating address");
                return false;
            }
        }

        public async Task<IdentityResult?> ChangePasswordAsync(ChangePasswordDto changePasswordDto, int userId)
        {
            try
            {
                var user = await _unitOfWork.AppUserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new NullReferenceException("User not found");

                var result = await _unitOfWork.AppUserRepository.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while changing password");
                return null;
            }
        }

        public async Task<bool> SendEmailConfirmationAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.AppUserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new NullReferenceException("User not found");
                if (string.IsNullOrEmpty(user.Email))
                    throw new NullReferenceException("Email not found");

                var token = await _unitOfWork.AppUserRepository.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = Uri.EscapeDataString(token);

                var confirmationLink = $"http://localhost:5000/api/account/confirm-email?userId={user.Id}&token={encodedToken}";
                var result = await _emailService.SendEmailAsync
                (
                    toEmail: user.Email,
                    subject: "Xác nhận email",
                    body: $"Hãy bấm vào <a href='{confirmationLink}'>đây</a> để xác nhận email của bạn"
                );
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while sending confimation email");
                return false;
            }
        }

        public async Task<IdentityResult?> ConfirmEmailAsync(int userId, string token)
        {
            try
            {
                var user = await _unitOfWork.AppUserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new NullReferenceException("User not found");

                var decodedToken = Uri.UnescapeDataString(token);
                var result = await _unitOfWork.AppUserRepository.ConfirmEmailAsync(user, decodedToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while confirming email");
                return null;
            }
        }

        public async Task LogoutAsync(RefreshTokenDto logoutDto)
        {
            try
            {
                var result = await _tokenService.RemoveRefreshTokenAsync(logoutDto.RefreshToken);
                if (!result)
                    throw new InvalidOperationException("Failed to logout");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while signing out");
            }
        }

        public async Task<TokenDto?> RefreshAsync(RefreshTokenDto refreshDto)
        {
            try
            {
                var user = await _tokenService.ValidateRefreshToken(refreshDto.RefreshToken);
                if (user == null)
                    throw new NullReferenceException("User not found");

                var accessToken = await _tokenService.GenerateAccessToken(user);
                var refreshToken = await _tokenService.GenerateRefreshToken(user);
                if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
                    return null;
                var tokenDto = new TokenDto { AccessToken = accessToken, RefreshToken = refreshToken };
                return tokenDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while refreshing");
                return null;
            }
        }

        public async Task<bool> SendPhoneNumberConfirmationCodeAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.AppUserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new NullReferenceException("User not found");
                if (string.IsNullOrEmpty(user.PhoneNumber))
                    throw new NullReferenceException("Phone number not found");

                var code = GenerateConfirmationCode();
                user.PhoneNumberConfirmationCode = code;
                user.PhoneNumberConfirmationCodeExpiresAt = DateTime.UtcNow.AddMinutes(5);
                await _unitOfWork.AppUserRepository.UpdateAsync(user);

                var message = $"Mã xác nhận số điện thoại của bạn là {code}";
                var result = await _smsService.SendSmsAsync(user.PhoneNumber, message);
                if (!result)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while sending phone number confirmation code");
                return false;
            }
        }

        public async Task<bool> ConfirmPhoneNumberAsync(ConfirmPhoneNumberDto confirmDto, int userId)
        {
            try
            {
                var user = await _unitOfWork.AppUserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new NullReferenceException("User not found");

                if (user.PhoneNumberConfirmationCode != confirmDto.Code || user.PhoneNumberConfirmationCodeExpiresAt < DateTime.UtcNow)
                    return false;
                user.PhoneNumberConfirmed = true;
                await _unitOfWork.AppUserRepository.UpdateAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while confirming phone number");
                return false;
            }
        }

        private string GenerateConfirmationCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
