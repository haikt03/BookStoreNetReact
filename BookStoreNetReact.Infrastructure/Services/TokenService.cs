using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Application.Options;
using BookStoreNetReact.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly ILogger<TokenService> _logger;
        public TokenService(IUnitOfWork unitOfWork, IOptions<JwtOptions> jwtOptions, ILogger<TokenService> logger)
        {
            _unitOfWork = unitOfWork;
            _jwtOptions = jwtOptions;
            _logger = logger;
        }

        public async Task<string> GenerateAccessToken(AppUser appUser)
        {
            try
            {
                if (appUser.UserName == null)
                    throw new NullReferenceException("UserName not found");
                if (appUser.Email == null)
                    throw new NullReferenceException("Email not found");

                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, appUser.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, appUser.UserName),
                    new Claim(ClaimTypes.Email, appUser.Email)
                };

                var roles = await _unitOfWork.AppUserRepository.GetRolesAsync(appUser);
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.TokenKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_jwtOptions.Value.MinutesExpired),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while generating access token");
                return "";
            }
        }

        public async Task<string> GenerateRefreshToken(AppUser appUser)
        {
            try
            {
                _unitOfWork.RefreshTokenRepository.RemoveExpiredToken();
                var refreshToken = new RefreshToken
                {
                    Token = GenerateTokenString(),
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    CreatedAt = DateTime.UtcNow,
                    UserId = appUser.Id
                };

                await _unitOfWork.RefreshTokenRepository.SaveToken(refreshToken);
                var result = await _unitOfWork.CompleteAsync();
                if (!result)
                    return "";
                return refreshToken.Token;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while generating refresh token");
                return "";
            }
        }

        public async Task<bool> RemoveRefreshTokenAsync(string refreshToken)
        {
            try
            {
                var token = await _unitOfWork.RefreshTokenRepository.GetToken(refreshToken);
                if (token == null)
                    throw new NullReferenceException("Token not found");

                _unitOfWork.RefreshTokenRepository.RemoveToken(token);
                var result = await _unitOfWork.CompleteAsync();
                if (!result)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while removing refresh token");
                return false;
            }
        }

        public async Task<AppUser?> ValidateRefreshToken(string refreshToken)
        {
            try
            {
                _unitOfWork.RefreshTokenRepository.RemoveExpiredToken();
                var token = await _unitOfWork.RefreshTokenRepository.GetValidToken(refreshToken);
                if (token == null)
                    throw new Exception("Invalid token");
                return token.User;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while validating refresh token");
                return null;
            }
        }

        private string GenerateTokenString()
        {
            try
            {
                var randomNumber = new byte[64];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while generating token string");
                return "";
            }
        }
    }
}
