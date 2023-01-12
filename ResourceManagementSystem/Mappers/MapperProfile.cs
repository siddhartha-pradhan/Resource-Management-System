using AutoMapper;
using ResourceManagementSystem.Core.Models;
using ResourceManagementSystem.Application.DTOs;

namespace ResourceManagementSystem.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryViewDto>();
            CreateMap<CategoryViewDto, Category>();
            CreateMap<CategoryPostDto, Category>();

            CreateMap<Product, ProductViewDto>();
            CreateMap<ProductInsertDto, Product>();
            CreateMap<ProductUpdateDto, Product>();

            CreateMap<OrderLinePostDto, OrderLine>();
            CreateMap<OrderPostDto, Order>();

            CreateMap<OrderLine, OrderLineViewDto>();
            CreateMap<Order, OrderViewDto>();

            CreateMap<Staff, StaffDto>();
        }
    }
}
