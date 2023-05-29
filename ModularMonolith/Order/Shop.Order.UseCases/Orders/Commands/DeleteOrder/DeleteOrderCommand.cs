using MediatR;

namespace Shop.Order.UseCases.Orders.Commands.DeleteOrder
{
    internal class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }
    }
}