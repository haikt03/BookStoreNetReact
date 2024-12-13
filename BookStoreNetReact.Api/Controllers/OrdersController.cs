using BookStoreNetReact.Api.Extensions;
using BookStoreNetReact.Application.Dtos.Order;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStoreNetReact.Api.Controllers
{
    [Route("api/orders")]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<PagedList<OrderDto>>> GetAllOrders([FromQuery] FilterOrderDto filterDto)
        {
            var ordersDto = await _orderService.GetAllWithFilterAsync(filterDto);
            if (ordersDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy đơn hàng" });
            Response.AddPaginationHeader(ordersDto.Pagination);
            return Ok(ordersDto);
        }

        [Authorize(Roles = "Member")]
        [HttpGet("me")]
        public async Task<ActionResult<PagedList<OrderDto>>> GetAllOrdersByMe([FromQuery] FilterOrderDto filterDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var ordersDto = await _orderService.GetAllWithFilterByUserIdAsync(filterDto, int.Parse(userId));
            if (ordersDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy đơn hàng" });

            Response.AddPaginationHeader(ordersDto.Pagination);
            return Ok(ordersDto);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<ActionResult<OrderDetailDto>> GetOrderById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var orderDto = await _orderService.GetByIdAsync(id, int.Parse(userId));
            if (orderDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy đơn hàng" });
            return Ok(orderDto);
        }

        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<ActionResult<OrderDetailDto>> CreateOrder([FromBody] CreateOrderDto createDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var orderDto = await _orderService.CreateAsync(createDto, int.Parse(userId));
            if (orderDto == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy đơn hàng" });

            return CreatedAtRoute("GetOrderById", new { id = orderDto.Id }, orderDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDetailDto>> UpdateOrderStatus([FromBody] UpdateOrderStatusDto updateOrderStatusDto, int id)
        {
            var orderDto = await _orderService.UpdateOrderStatusAsync(updateOrderStatusDto, id);
            if (orderDto == null)
                return BadRequest(new ProblemDetails { Title = "Cập nhật trạng thái đơn hàng không thành công" });
            return orderDto;
        }

        [Authorize]
        [HttpGet("filter")]
        public async Task<ActionResult<OrderFilterDto>> GetFilter()
        {
            var filterDto = await _orderService.GetFilterAsync();
            if (filterDto == null)
                return BadRequest(new ProblemDetails { Title = "Không tìm thấy bộ lọc đơn hàng" });
            return Ok(filterDto);
        }
    }
}
