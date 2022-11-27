using AutoMapper;
using DataAccess.Domain;
using RestaurantApi.Models;

namespace RestaurantApi.MapperProfiles;

/// <summary>
/// Mapper for Product entity
/// </summary>
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductUpdateDto, Product>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<Product, ProductViewDto>();
        CreateMap<Section, SectionViewDto>();
        CreateMap<Menu, MenuViewDto>();
        CreateMap<Order, OrderViewDto>();
        CreateMap<OrderDto, Order>();
    }
}