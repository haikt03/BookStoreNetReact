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
        public async Task<ActionResult<AppUserWithTokenDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _appUserService.LoginAsync(loginDto);
            if (user == null)
                return Unauthorized("Sai tài khoản hoặc mật khẩu");
            return Ok(user);
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
                return ValidationProblem();
            }
            return StatusCode(201);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<DetailAppUserDto>> GetMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();
            var user = await _appUserService.GetUserByIdAsync(int.Parse(userId));
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
            var result = await _appUserService.UpdateUserAsync(updateDto, int.Parse(userId));
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

            var result = await _appUserService.UpdateUserAddressAsync(updateDto, int.Parse(userId));
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
        public async Task<ActionResult> ConfirmEmail([FromQuery]ConfirmEmailDto confirmDto)
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

        [Authorize]
        [HttpPost("me/logout")]
        public async Task<ActionResult> Logout([FromBody] RefreshTokenDto logoutDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            await _appUserService.LogoutAsync(logoutDto);
            return Ok();
        }

        [HttpPost("me/refresh")]
        public async Task<ActionResult<TokenDto>> Refresh([FromBody] RefreshTokenDto refreshDto)
        {
            var tokenDto = await _appUserService.RefreshAsync(refreshDto);
            if (tokenDto == null)
                return Unauthorized();
            return Ok(tokenDto);
        }
    }
}
