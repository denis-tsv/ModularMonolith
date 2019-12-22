using AutoMapper;
using Shop.Order.Entities;
using Shop.Order.UseCases.Orders.Dto;

namespace Shop.Order.UseCases.Orders.Mappings
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
