using BookStoreNetReact.Application.Dtos.AppUser;

namespace BookStoreNetReact.Application.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public double Amount { get; set; }
        public string PaymentStatus { get; set; } = "";
        public string OrderStatus { get; set; } = "";
        public DateTime OrderDate { get; set; }
        public AppUserDto User { get; set; } = new AppUserDto();
    }
}
