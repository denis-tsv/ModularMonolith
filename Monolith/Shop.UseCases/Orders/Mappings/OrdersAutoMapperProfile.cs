using AutoMapper;
using Shop.Entities;
using Shop.UseCases.Orders.Dto;

namespace Shop.UseCases.Orders.Mappings
{
    public  class OrdersAutoMapperProfile : Profile
    {
        public OrdersAutoMapperProfile()
        {
            CreateMap<Entities.Order, OrderDto>()
                .ForMember(x => x.Price, opt => opt.Ignore());

            CreateMap<CreateOrderDto, Entities.Order>();
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
