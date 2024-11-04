using AutoMapper;
using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class AppUserService : GenericService<AppUserService>, IAppUserService
    {
        private readonly ITokenService _tokenService;
        public AppUserService(IMapper mapper, ICloudUploadService cloudUploadService, IUnitOfWork unitOfWork, ILogger<AppUserService> logger, ITokenService tokenService) : base(mapper, cloudUploadService, unitOfWork, logger)
        {
            _tokenService = tokenService;
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var user = _mapper.Map<AppUser>(registerDto);
                var result =await _unitOfWork.AppUserRepository.AddAsync(user);
                if (!result)
                    throw new InvalidOperationException("Failed to save changes app user data");
                return true;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Failed to save changes app user data");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while creating app user");
                return false;
            }
        }

        public async Task<AppUserWithTokenDto?> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var appUser = await _unitOfWork.AppUserRepository.GetByUserNameAsync(loginDto.UserName);
                if (appUser == null || !await _unitOfWork.AppUserRepository.CheckPasswordAsync(appUser, loginDto.Password))
                    return null;

                var result = _mapper.Map<AppUserWithTokenDto>(appUser);
                var accessToken = await _tokenService.GenerateAccessToken(appUser);
                var refreshToken = await _tokenService.GenerateRefreshToken(appUser);
                if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
                    return null;

                result.AccessToken = accessToken;
                result.RefreshToken = refreshToken;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while singing in");
                return null;
            }
        }
    }
}
