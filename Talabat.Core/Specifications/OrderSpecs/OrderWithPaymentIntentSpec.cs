using Talabat.Core.Entities.Order;

namespace Talabat.Core.Specifications.OrderSpecs
{
    public class OrderWithPaymentIntentSpec : BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentSpec(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
