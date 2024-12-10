using BookStoreNetReact.Application.Dtos.Pagination;

namespace BookStoreNetReact.Application.Dtos.Order
{
    public class FilterOrderDto : FilterDto
    {
        public string OrderStatuses { get; set; } = "";
        public string PaymentStatuses { get; set; } = "";
        public string CodeSearch { get; set; } = "";
        public string UserSearch { get; set; } = "";
        public int MinAmount { get; set; } = 0;
        public int MaxAmount { get; set; } = 0;
        public DateOnly? OrderDateStart { get; set; } = null;
        public DateOnly? OrderDateEnd { get; set; } = null;
    }
}
