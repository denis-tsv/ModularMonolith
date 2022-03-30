using MediatR;
using Shop.Order.UseCases.Orders.Dto;

namespace Shop.Order.UseCases.Orders.Queries.GetOrder
{
    internal class GetOrderRequest : IRequest<OrderDto>, IOrderRequest
    {
        public int Id { get; set; }
    }
}
