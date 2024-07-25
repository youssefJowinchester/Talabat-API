using Talabat.Core.Entities.Order;

namespace Talabat.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> createOrderAsync(string BuyerEmail, string basketId, int DeliveryMethodId, Address ShippingAddress);

        Task<IReadOnlyList<Order>?> GetOrdersForSpecUserAsync(string BuyerEmail);

        Task<Order?> GetOrderByIdForSpecUSerAsync(string BuyerEmail, int OrderId);
    }
}
