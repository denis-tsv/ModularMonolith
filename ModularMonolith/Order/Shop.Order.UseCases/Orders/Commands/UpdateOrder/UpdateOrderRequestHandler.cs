using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Order.UseCases.Orders.Commands.UpdateOrder
{
    internal class UpdateOrderRequestHandler : IRequestHandler<UpdateOrderRequest>
    {
        public Task Handle(UpdateOrderRequest request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
