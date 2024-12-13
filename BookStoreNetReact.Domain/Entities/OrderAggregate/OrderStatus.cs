using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        [Display(Name = "Chờ xác nhận")]
        PendingConfirmation,
        [Display(Name = "Chờ lấy hàng")]
        PendingPickup,
        [Display(Name = "Chờ giao hàng")]
        PendingDelivery,
        [Display(Name = "Đã giao hàng")]
        Delivered,
        [Display(Name = "Chờ trả hàng")]
        PendingReturn,
        [Display(Name = "Đã trả")]
        Returned,
        [Display(Name = "Đã huỷ đơn")]
        Cancelled
    }
}
