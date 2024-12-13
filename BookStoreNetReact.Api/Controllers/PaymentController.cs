using BookStoreNetReact.Application.Dtos.Basket;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Application.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System.Security.Claims;

namespace BookStoreNetReact.Api.Controllers
{
    [Route("api/payment")]
    public class PaymentController : BaseApiController
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;
        private readonly IOptions<PaymentOptions> _paymentOptions;
        public PaymentController(IPaymentService paymentService, IBasketService basketService, IOptions<PaymentOptions> paymentOptions, IOrderService orderService) 
        { 
            _basketService = basketService;
            _paymentOptions = paymentOptions;
            _orderService = orderService;
        }

        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var basketDto = await _basketService.CreateOrUpdatePaymentIntent(int.Parse(userId));
            if (basketDto == null)
                return BadRequest(new ProblemDetails { Title = "Khởi tạo thanh toán không thành công" });
            return Ok(basketDto);
        }

        [AllowAnonymous]
        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
            _paymentOptions.Value.WhSecret);

            var charge = (Charge)stripeEvent.Data.Object;

            var result = await _orderService.UpdatePaymentStatusAsync(charge);
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Lỗi StripeWebhook" });
            return new EmptyResult();
        }
    }
}
