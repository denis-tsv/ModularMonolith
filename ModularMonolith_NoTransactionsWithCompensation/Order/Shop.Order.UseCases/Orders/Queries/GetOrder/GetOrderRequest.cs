using MediatR;
using Shop.Order.Contract.Dto;

namespace Shop.Order.UseCases.Orders.Queries.GetOrder
{
    internal class GetOrderRequest : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }
}
