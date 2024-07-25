using Talabat.Core.Entities.Order;

namespace Talabat.Core.Specifications.OrderSpecs
{
    public class OrderSpec : BaseSpecifications<Order>
    {
        public OrderSpec(string email) : base(o => o.BuyerEmail == email)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderByDesc(o => o.OrderDate);
        }
        public OrderSpec(string email, int orderId) : base(o => o.BuyerEmail == email && o.Id == orderId)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }

    }
}
