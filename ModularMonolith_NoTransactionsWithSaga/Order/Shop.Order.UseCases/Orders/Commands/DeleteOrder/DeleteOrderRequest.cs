using MediatR;

namespace Shop.Order.UseCases.Orders.Commands.DeleteOrder
{
    public class DeleteOrderRequest : IRequest
    {
        public int Id { get; set; }
    }
}
