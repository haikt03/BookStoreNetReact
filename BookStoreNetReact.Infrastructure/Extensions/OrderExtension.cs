using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Domain.Entities.OrderAggregate;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class OrderExtension
    {
        public static IQueryable<Order> Search(this IQueryable<Order> query, string? codeSearch, string? userSearch)
        {
            if (string.IsNullOrWhiteSpace(codeSearch) && string.IsNullOrWhiteSpace(userSearch))
                return query;
            var lowerCaseCodeSearch = codeSearch?.Trim().ToLower();
            var lowerCaseUserSearch = userSearch?.Trim().ToLower();
            var result = query
                .Where(o => string.IsNullOrWhiteSpace(lowerCaseCodeSearch) 
                    || o.Code.ToLower().Contains(lowerCaseCodeSearch))
                .Where(o => string.IsNullOrWhiteSpace(lowerCaseUserSearch) || o.User == null
                    || o.User.FullName.ToLower().Contains(lowerCaseUserSearch));
            return result;
        }

        public static IQueryable<Order> Filter
        (
            this IQueryable<Order> query,
            string? orderStatuses,
            string? paymentStatuses,
            int minAmount = 0,
            int maxAmount = 0,
            DateOnly? orderDateStart = null,
            DateOnly? orderDateEnd = null
        )
        {
            var orderStatusList = string.IsNullOrWhiteSpace(orderStatuses)
                ? new List<OrderStatus>()
                : orderStatuses.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(status => Enum.TryParse<OrderStatus>(status.Trim(), true, out var parsedStatus)
                                        ? parsedStatus
                                        : (OrderStatus?)null)
                    .Where(status => status.HasValue)
                    .Select(status => status!.Value)
                    .ToList();
            var paymentStatusList = string.IsNullOrWhiteSpace(paymentStatuses)
                ? new List<PaymentStatus>()
                : paymentStatuses.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(status => Enum.TryParse<PaymentStatus>(status.Trim(), true, out var parsedStatus)
                                        ? parsedStatus
                                        : (PaymentStatus?)null)
                    .Where(status => status.HasValue)
                    .Select(status => status!.Value)
                    .ToList();

            var result = query
                .Where(o => !orderStatusList.Any() || orderStatusList.Contains(o.OrderStatus))
                .Where(b => (minAmount == 0 || b.Amount >= minAmount))
                .Where(b => (maxAmount == 0 || b.Amount <= maxAmount))
                .Where(b => (!orderDateStart.HasValue || b.CreatedAt >= orderDateStart.Value.ToDateTime(TimeOnly.MinValue)))
                .Where(b => (!orderDateEnd.HasValue || b.CreatedAt <= orderDateEnd.Value.ToDateTime(TimeOnly.MaxValue)));
            return result;
        }

        public static IQueryable<Order> Sort(this IQueryable<Order> query, string? sort = null)
        {
            if (string.IsNullOrWhiteSpace(sort))
            {
                return query.OrderByDescending(b => b.CreatedAt);
            }
            var result = sort switch
            {
                "orderDateAsc" => query.OrderBy(b => b.CreatedAt),
                "orderDateDesc" => query.OrderByDescending(b => b.CreatedAt),
                "amountAsc" => query.OrderBy(b => b.Amount),
                "amountDesc" => query.OrderByDescending(b => b.Amount),
                _ => query.OrderByDescending(b => b.CreatedAt)
            };
            return result;
        }
    }
}
