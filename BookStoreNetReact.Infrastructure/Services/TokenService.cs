using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Application.Options;
using BookStoreNetReact.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly ILogger<TokenService> _logger;
        public TokenService(UserManager<AppUser> userManager, IOptions<JwtOptions> jwtOptions, ILogger<TokenService> logger)
        {
            _userManager = userManager;
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

                var roles = await _userManager.GetRolesAsync(appUser);
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
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "UserName or Email data not found");
                return "";
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
                appUser.RefreshTokens?.RemoveAll(rt => rt.ExpiresAt < DateTime.UtcNow);
                var refreshToken = new RefreshToken
                {
                    Token = GenerateTokenString(),
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    CreatedAt = DateTime.UtcNow,
                    UserId = appUser.Id
                };

                if (appUser.RefreshTokens == null)
                    appUser.RefreshTokens = new List<RefreshToken>();
                appUser.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(appUser);

                return refreshToken.Token;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while generating refresh token");
                return "";
            }
        }

        public async Task<bool> ValidateRefreshToken(AppUser appUser, string refreshToken)
        {
            try
            {
                appUser.RefreshTokens?.RemoveAll(t => t.ExpiresAt < DateTime.UtcNow);
                var token = appUser.RefreshTokens?.FirstOrDefault(t => t.Token == refreshToken && t.ExpiresAt > DateTime.UtcNow && t.RevokedAt == null);
                await _userManager.UpdateAsync(appUser);
                return token != null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while validating refresh token");
                return false;
            }
        }

        private string GenerateTokenString()
        {
            try
            {
                var randomNumber = new byte[32];
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
