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
using System.Net;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class AppUserService : GenericService<AppUserService>, IAppUserService
    {
        private readonly ITokenService _tokenService;
        public AppUserService(IMapper mapper, ICloudUploadService cloudUploadService, IUnitOfWork unitOfWork, ILogger<AppUserService> logger, ITokenService tokenService) : base(mapper, cloudUploadService, unitOfWork, logger)
        {
            _tokenService = tokenService;
        }

        public async Task<IdentityResult?> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var user = _mapper.Map<AppUser>(registerDto);
                var result = await _unitOfWork.AppUserRepository.AddAsync(user, registerDto.Password);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while creating app user");
                return null;
            }
        }

        public async Task<AppUserWithTokenDto?> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _unitOfWork.AppUserRepository.GetByUserNameAsync(loginDto.UserName);
                if (user == null || !await _unitOfWork.AppUserRepository.CheckPasswordAsync(user, loginDto.Password))
                    return null;

                var userWithToken = _mapper.Map<AppUserWithTokenDto>(user);
                var accessToken = await _tokenService.GenerateAccessToken(user);
                var refreshToken = await _tokenService.GenerateRefreshToken(user);
                if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
                    return null;

                userWithToken.AccessToken = accessToken;
                userWithToken.RefreshToken = refreshToken;
                return userWithToken;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while singing in");
                return null;
            }
        }

        public async Task<PagedList<AppUserDto>?> GetAllUsersAsync(FilterAppUserDto filterDto)
        {
            try
            {
                var users = _unitOfWork.AppUserRepository.GetAll(filterDto);
                if (users == null)
                    throw new NullReferenceException("Users not found");

                var result = await users.ToPagedListAsync
                    (
                        selector: au => _mapper.Map<AppUserDto>(au),
                        pageSize: filterDto.PageSize,
                        pageIndex: filterDto.PageIndex,
                        logger: _logger
                    );
                return result;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Users data not found");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting app users");
                return null;
            }
        }

        public async Task<DetailAppUserDto?> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.AppUserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new NullReferenceException("User not found");
                var userDto = _mapper.Map<DetailAppUserDto>(user);
                return userDto;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "User data not found");
                return null;
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
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "User data not found");
                return null;
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
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "User data not found");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while deleting user");
                return null;
            }
        }

        public async Task<bool> UpdateUserAddressAsync(UpdateUserAddressDto updateDto, int addressId)
        {
            try
            {
                var address = await _unitOfWork.AppUserRepository.GetUserAddressByIdAsync(addressId);
                if (address == null)
                    throw new NullReferenceException("Address not found");

                _mapper.Map(updateDto, address);
                _unitOfWork.AppUserRepository.UpdateUserAddress(address);
                var result = await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Address data not found");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while updating user address");
                return false;
            }
        }
    }
}
