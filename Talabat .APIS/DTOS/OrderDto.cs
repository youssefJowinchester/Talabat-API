using System.ComponentModel.DataAnnotations;

namespace Talabat_.APIS.DTOS
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }

        [Required]
        public int DeliveryMethodId { get; set; }

        [Required]
        public AddressDto ShippingToAddress { get; set; }
    }
}
