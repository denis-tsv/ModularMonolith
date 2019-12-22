using MediatR;
using Shop.Order.Contract.Orders.Dto;

namespace Shop.Order.UseCases.Orders.Queries.GetOrder
{
    internal class GetOrderRequest : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }
}
