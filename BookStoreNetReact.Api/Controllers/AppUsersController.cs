using BookStoreNetReact.Api.Extensions;
using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreNetReact.Api.Controllers
{
    [Route("api/appUsers")]
    public class AppUsersController : BaseApiController
    {
        private readonly IAppUserService _appUserService;
        public AppUsersController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<PagedList<AppUserDto>>> GetAllUsers([FromQuery] FilterAppUserDto filterDto)
        {
            var usersDto = await _appUserService.GetAllUsersAsync(filterDto);
            if (usersDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy người dùng" });
            Response.AddPaginationHeader(usersDto.Pagination);
            return Ok(usersDto);
        }

        [HttpGet("{id}", Name = nameof(GetUserById))]
        public async Task<ActionResult<DetailAppUserDto>> GetUserById(int id)
        {
            var userDto = await _appUserService.GetUserByIdAsync(id);
            if (userDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy người dùng" });
            return Ok(userDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser([FromForm] UpdateAppUserDto updateDto, int id)
        {
            var result = await _appUserService.UpdateUserAsync(updateDto, id);
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var result = await _appUserService.DeleteUserAsync(id);
            if (result == null)
            {
                return BadRequest(new ProblemDetails { Title = "Xóa người dùng không thành công" });
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
