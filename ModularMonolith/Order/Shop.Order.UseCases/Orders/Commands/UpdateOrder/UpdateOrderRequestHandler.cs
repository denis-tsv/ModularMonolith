using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Order.UseCases.Orders.Commands.UpdateOrder
{
    internal class UpdateOrderRequestHandler : AsyncRequestHandler<UpdateOrderRequest>
    {
        protected override Task Handle(UpdateOrderRequest request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
