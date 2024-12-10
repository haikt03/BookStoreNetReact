namespace BookStoreNetReact.Application.Dtos.Order
{
    public class OrderFilterDto
    {
        public List<string>? OrderStatuses { get; set; }
        public List<string>? PaymentStatuses { get; set; }
        public required double MinAmount { get; set; }
        public required double MaxAmount { get; set; }
    }
}
