using AutoMapper;
using Shop.Order.Contract.Orders.Dto;
using Shop.Order.Entities;

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
