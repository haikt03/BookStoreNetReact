using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Entities.OrderAggregate
{
    public enum PaymentStatus
    {
        [Display(Name = "Chờ xử lý")]
        Pending,
        [Display(Name = "Thành công")]
        Completed,
        [Display(Name = "Thất bại")]
        Failed
    }
}
