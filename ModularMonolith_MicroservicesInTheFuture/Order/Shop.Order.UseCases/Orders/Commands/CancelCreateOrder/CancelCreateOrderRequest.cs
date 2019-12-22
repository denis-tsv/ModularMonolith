using MediatR;

namespace Shop.Order.UseCases.Orders.Commands.CancelCreateOrder
{
    internal class CancelCreateOrderRequest : IRequest
    {
        public int Id { get; set; }
    }
}
