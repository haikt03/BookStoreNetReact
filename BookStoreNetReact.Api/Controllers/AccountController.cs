using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Application.Dtos.UserAddress;
using BookStoreNetReact.Application.Interfaces.Services;
using EasyCaching.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStoreNetReact.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : BaseApiController
    {
        private readonly IAppUserService _appUserService;
        private readonly IEasyCachingProvider _cachingProvider;

        public AccountController(IAppUserService appUserService, IEasyCachingProviderFactory cachingProviderFactory)
        {
            _appUserService = appUserService;
            _cachingProvider = cachingProviderFactory.GetCachingProvider("default");
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserDetailDto>> Login([FromBody] LoginDto loginDto)
        {
            var userWithToken = await _appUserService.LoginAsync(loginDto);
            if (userWithToken == null)
                return BadRequest(new ProblemDetails { Title = "Sai tài khoản hoặc mật khẩu" });

            AddTokenToCookies(userWithToken.Token);
            return Ok(userWithToken.User);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _appUserService.RegisterAsync(registerDto);
            if (result == null)
                return BadRequest(new ProblemDetails { Title = "Đăng ký không thành công" });

            if (result != null && !result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem(new ValidationProblemDetails(ModelState)
                {
                    Title = "Đăng ký không thành công"
                });
            }
            return StatusCode(201);
        }

        [Authorize]
        [HttpPost("me/logout")]
        public async Task<ActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();
            var logoutDto = new RefreshTokenDto { RefreshToken = Request.Cookies["refreshToken"] ?? "" };
            await _appUserService.LogoutAsync(logoutDto);
            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");
            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh()
        {
            var refreshDto = new RefreshTokenDto { RefreshToken = Request.Cookies["refreshToken"] ?? "" };
            var tokenDto = await _appUserService.RefreshAsync(refreshDto);
            if (tokenDto == null)
                return BadRequest(new ProblemDetails { Title = "Vui lòng đăng nhập lại" });

            AddTokenToCookies(tokenDto);
            return Ok();
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<AppUserDetailDto>> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var cacheKey = "current-user-" + userId;
            var cacheData = await _cachingProvider.GetAsync<AppUserDetailDto>(cacheKey);
            if (cacheData.HasValue)
                return Ok(cacheData.Value);

            var user = await _appUserService.GetByIdAsync(int.Parse(userId));
            if (user == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy người dùng" });
            await _cachingProvider.SetAsync(cacheKey, user, TimeSpan.FromMinutes(10));
            return Ok(user);
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<ActionResult> UpdateMe([FromForm] UpdateAppUserDto updateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();
            var result = await _appUserService.UpdateAsync(updateDto, int.Parse(userId));
            if (result == null)
                return BadRequest(new ProblemDetails { Title = "Cập nhật người dùng không thành công" });

            if (result != null && !result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }

            var updatedUser = await _appUserService.GetByIdAsync(int.Parse(userId));
            if (updatedUser != null)
            {
                var cacheKey = "current-user-" + userId;
                await _cachingProvider.SetAsync(cacheKey, updatedUser, TimeSpan.FromMinutes(10));
            }
            return Ok();
        }

        [Authorize]
        [HttpPut("me/address")]
        public async Task<ActionResult> UpdateUserAddress([FromBody] UpdateUserAddressDto updateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var result = await _appUserService.UpdateAddressAsync(updateDto, int.Parse(userId));
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Cập nhật địa chỉ người dùng không thành công" });

            var updatedUser = await _appUserService.GetByIdAsync(int.Parse(userId));
            if (updatedUser != null)
            {
                var cacheKey = "current-user-" + userId;
                await _cachingProvider.SetAsync(cacheKey, updatedUser, TimeSpan.FromMinutes(10));
            }
            return Ok();
        }

        [Authorize]
        [HttpPut("me/password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var result = await _appUserService.ChangePasswordAsync(changePasswordDto, int.Parse(userId));
            if (result == null)
                return BadRequest(new ProblemDetails { Title = "Cập nhật mật khẩu không thành công" });

            if (result != null && !result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
            return Ok();
        }

        [Authorize]
        [HttpPost("me/send-confirmation-email")]
        public async Task<ActionResult> SendConfirmationEmail()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();
            var result = await _appUserService.SendEmailConfirmationAsync(int.Parse(userId));
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Gửi email xác nhận không thành công" });
            return Ok();
        }

        [HttpGet("confirm-email")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] ConfirmEmailDto confirmDto)
        {
            var result = await _appUserService.ConfirmEmailAsync(confirmDto.UserId, confirmDto.Token);
            if (result == null)
                return BadRequest(new ProblemDetails { Title = "Xác nhận email không thành công" });

            if (result != null && !result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
            return Ok("Xác nhận email thành công");
        }

        [Authorize]
        [HttpPost("me/send-confirmation-sms")]
        public async Task<ActionResult> SendConfirmationSms()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();
            var result = await _appUserService.SendPhoneNumberConfirmationCodeAsync(int.Parse(userId));
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Gửi sms xác nhận không thành công" });
            return Ok();
        }

        [Authorize]
        [HttpGet("me/confirm-phone-number")]
        public async Task<ActionResult> ConfirmPhoneNumber([FromBody] ConfirmPhoneNumberDto confirmDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var result = await _appUserService.ConfirmPhoneNumberAsync(confirmDto, int.Parse(userId));
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Xác nhận số điện thoại không thành công" });
            return Ok("Xác nhận email thành công");
        }

        private void AddTokenToCookies(TokenDto tokenDto)
        {
            Response.Cookies.Append("accessToken", tokenDto.AccessToken, new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddMinutes(30)
            });
            Response.Cookies.Append("refreshToken", tokenDto.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddDays(7)
            });
        }
    }
}
