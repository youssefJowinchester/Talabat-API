using AutoMapper;
using Talabat.Core.Entities.Order;
using Talabat_.APIS.DTOS;

namespace Talabat_.APIS.Helpers
{
    public class OrderItempictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItempictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{_configuration["BaseUrl"]}/{source.Product.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
