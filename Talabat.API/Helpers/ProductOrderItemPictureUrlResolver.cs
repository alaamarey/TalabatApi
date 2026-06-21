using AutoMapper;
using Talabat.API.Dtos;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.API.Helpers
{
    public class ProductOrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductOrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.ProductPictureURL))
                return $"{_configuration["ApiHost"]}/images/{source.Product.ProductPictureURL}";


            return string.Empty;
        }
    }
}
