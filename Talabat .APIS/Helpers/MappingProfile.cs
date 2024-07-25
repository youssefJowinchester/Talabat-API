using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat_.APIS.DTOS;
using OrderAddress = Talabat.Core.Entities.Order.Address;
using orders = Talabat.Core.Entities.Order.Order;
using UserAddress = Talabat.Core.Entities.Identity.Address;

namespace Talabat_.APIS.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, productToReturnDTO>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<productUrlResolver>());

            CreateMap<UserAddress, AddressDto>().ReverseMap();

            CreateMap<OrderAddress, AddressDto>().ReverseMap()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LName));


            CreateMap<orders, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItempictureUrlResolver>());

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
        }
    }
}
