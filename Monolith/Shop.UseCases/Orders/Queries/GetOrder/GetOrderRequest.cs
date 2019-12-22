using MediatR;
using Shop.UseCases.Orders.Dto;

namespace Shop.UseCases.Orders.Queries.GetOrder
{
    public class GetOrderRequest : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }
}
