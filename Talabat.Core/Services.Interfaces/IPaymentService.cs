using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);

        Task<Order> UpdatePaymentIntentToSuccessedOrFailed(string PaymentIntentId, bool flag);
    }
}
