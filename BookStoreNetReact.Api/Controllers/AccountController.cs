using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Application.Dtos.UserAddress;
using BookStoreNetReact.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStoreNetReact.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : BaseApiController
    {
        private readonly IAppUserService _appUserService;
        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserDetailDto>> Login([FromBody] LoginDto loginDto)
        {
            var userWithToken = await _appUserService.LoginAsync(loginDto);
            if (userWithToken == null)
                return BadRequest(new ProblemDetails { Title = "Sai tài khoản hoặc mật khẩu" });

            Response.Cookies.Append("accessToken", userWithToken.Token.AccessToken, new CookieOptions
            {
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddMinutes(30)
            });
            Response.Cookies.Append("refreshToken", userWithToken.Token.RefreshToken, new CookieOptions
            {
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddDays(7)
            });
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
        public async Task<ActionResult> Logout([FromBody] RefreshTokenDto logoutDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            await _appUserService.LogoutAsync(logoutDto);
            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");
            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenDto refreshDto)
        {
            var tokenDto = await _appUserService.RefreshAsync(refreshDto);
            if (tokenDto == null)
                return Unauthorized();
            Response.Cookies.Append("accessToken", tokenDto.AccessToken, new CookieOptions
            {
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddMinutes(30)
            });
            Response.Cookies.Append("refreshToken", tokenDto.RefreshToken, new CookieOptions
            {
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddDays(7)
            });
            return Ok();
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<AppUserDetailDto>> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();
            var user = await _appUserService.GetByIdAsync(int.Parse(userId));
            if (user == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy người dùng" });
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
            return Ok();
        }

        [Authorize]
        [HttpPut("me/address")]
        public async Task<ActionResult> UpdateUserAddres([FromBody] UpdateUserAddressDto updateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var result = await _appUserService.UpdateAddressAsync(updateDto, int.Parse(userId));
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Cập nhật địa chỉ người dùng không thành công" });
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
    }
}
