using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

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
            var appUser = await _appUserService.LoginAsync(loginDto);
            if (appUser == null)
                return Unauthorized();
            return Ok(appUser);
        }
    }
}
