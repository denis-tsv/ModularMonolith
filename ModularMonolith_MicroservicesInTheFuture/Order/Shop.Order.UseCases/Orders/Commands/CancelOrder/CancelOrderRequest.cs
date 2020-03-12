using MediatR;

namespace Shop.Order.UseCases.Orders.Commands.CancelOrder
{
    internal class CancelOrderRequest : IRequest
    {
        public int Id { get; set; }
    }
}
