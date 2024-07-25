using AutoMapper;
using Talabat.Core.Entities;
using Talabat_.APIS.DTOS;

namespace Talabat_.APIS.Helpers
{
    public class productUrlResolver : IValueResolver<Product, productToReturnDTO, string>
    {
        private readonly IConfiguration _configuration;

        public productUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, productToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["BaseUrl"]}/{source.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
