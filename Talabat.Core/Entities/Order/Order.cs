namespace Talabat.Core.Entities.Order
{
    public class Order : BaseEntity
    {
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod,
            ICollection<OrderItem> items, decimal suTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SuTotal = suTotal;
            PaymentIntentId = paymentIntentId;
        }

        public Order()
        {

        }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal SuTotal { get; set; }

        public string PaymentIntentId { get; set; }

        //[NotMapped]
        //public decimal Total { get => SuTotal + DeliveryMethod.Cost; }

        public decimal GetTotal() => SuTotal + DeliveryMethod.Cost;
    }
}
