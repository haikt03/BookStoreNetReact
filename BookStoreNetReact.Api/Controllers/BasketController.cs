using BookStoreNetReact.Application.Dtos.Basket;
using BookStoreNetReact.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStoreNetReact.Api.Controllers
{
    [Route("api/basket")]
    public class BasketController : BaseApiController
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [Authorize(Roles = "Member")]
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var basket = await _basketService.GetByUserIdAsync(int.Parse(userId));
            if (basket == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy giỏ hàng" });
            return Ok(basket);
        }

        [Authorize(Roles = "Member")]
        [HttpPost("add")]
        public async Task<ActionResult<BasketDto>> AddBasketItem([FromQuery] UpdateQuantityDto updateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var basketDto = await _basketService.UpdateQuantityAsync(updateDto, int.Parse(userId), "plus");
            if (basketDto == null)
                return BadRequest(new ProblemDetails { Title = "Thêm sản phẩm vảo giỏ hàng không thành công" });
            return Ok(basketDto);
        }

        [Authorize(Roles = "Member")]
        [HttpPost("remove")]
        public async Task<ActionResult<BasketDto>> RemoveBasketItem([FromQuery] UpdateQuantityDto updateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var basketDto = await _basketService.UpdateQuantityAsync(updateDto, int.Parse(userId), "minus");
            if (basketDto == null)
                return BadRequest(new ProblemDetails { Title = "Xoá bớt sản phẩm khỏi giỏ hàng không thành công" });
            return Ok(basketDto);
        }
    }
}
