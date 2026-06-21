using AutoMapper;
using Talabat.API.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.BrandName, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.ProductCategory.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());


            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<Order, OrdertoReturnDto>()
                .ForMember(d => d.DeliveryMethodName, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodPrice, o => o.MapFrom(s => s.DeliveryMethod.Cost));


            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.ProductDescription, o => o.MapFrom(s => s.Product.ProductDescription))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.Product.ProductBrand))
                .ForMember(d => d.ProductCategory, o => o.MapFrom(s => s.Product.ProductCategory))
                .ForMember(d => d.ProductPictureURL, o => o.MapFrom<ProductOrderItemPictureUrlResolver>());




            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(d => d.UserName , o=> o.MapFrom(s => s.DisplayName))
                .ForMember( d => d.PasswordHash , o=> o.MapFrom(s => s.Password));





        }
    }
}
