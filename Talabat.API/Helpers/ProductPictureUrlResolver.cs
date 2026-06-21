using AutoMapper;
using Talabat.API.Dtos;
using Talabat.Core.Entities;

namespace Talabat.API.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["ApiHost"]}/images/{source.PictureUrl}";

            return string.Empty;
        }
    }
}
