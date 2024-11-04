﻿using Microsoft.AspNetCore.Mvc;

namespace BookStoreNetReact.Api.Controllers
{
    [Route("api/buggy")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ProblemDetails { Title = "Thao tác không thành công" });
        }

        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            return NotFound(new ProblemDetails { Title = "Không tìm thấy" });
        }

        [HttpGet("server-error")]
        public IActionResult GetInternalServerlError()
        {
            throw new Exception("Lỗi máy chủ");
        }
    }
}