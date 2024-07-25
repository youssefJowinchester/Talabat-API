namespace Talabat.Core.Entities.Order
{
    public class DeliveryMethod : BaseEntity
    {
        public DeliveryMethod(string shortName, string descraption, string deliveryTime, decimal cost)
        {
            ShortName = shortName;
            Descraption = descraption;
            DeliveryTime = deliveryTime;
            Cost = cost;
        }

        public DeliveryMethod()
        {

        }
        public string? ShortName { get; set; } = null;

        public string Descraption { get; set; }
        public string DeliveryTime { get; set; }

        public decimal Cost { get; set; }
    }
}
