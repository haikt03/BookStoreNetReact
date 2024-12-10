namespace BookStoreNetReact.Domain.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        PendingConfirmation,
        PendingPickup,
        PendingDelivery,
        Returned,
        Delivered,
        Cancelled
    }
}
