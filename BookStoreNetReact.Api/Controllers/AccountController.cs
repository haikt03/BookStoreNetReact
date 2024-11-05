using BookStoreNetReact.Application.Dtos.AppUser;
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
                return Unauthorized();
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _appUserService.RegisterAsync(registerDto);
            if (result == null)
            {
                return BadRequest(new ProblemDetails { Title = "Đăng ký không thành công"});
            }
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
            {
                return BadRequest(new ProblemDetails { Title = "Cập nhật người dùng không thành công" });
            }
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
    }
}
