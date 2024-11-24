using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Basket.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Orders.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Products.Models;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Identity;

using userAddress = LinkDev.Talabat.Core.Domain.Entities.Identity.Address;
using OrderAddress = LinkDev.Talabat.Core.Domain.Entities.Orders.Address;

namespace LinkDev.Talabat.Core.Application.Mapping
{
	internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, O => O.MapFrom(s => s.Brand!.Name))
                .ForMember(d => d.Category, O => O.MapFrom(s => s.Category!.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod!.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom(src => src.Product.PictureUrl))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<OrderAddress, AddressDto>().ReverseMap();

			CreateMap<DeliveryMethod, DeliveryMethodDto>();

            CreateMap<userAddress, AddressDto>();
        }
    }
}
