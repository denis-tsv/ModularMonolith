using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Commands.DeleteOrder
{
    internal class DeleteOrderRequestHandler : AsyncRequestHandler<DeleteOrderRequest>
    {
        private readonly IOrderDbContext _context;

        public DeleteOrderRequestHandler(IOrderDbContext context)
        {
            _context = context;
        }
        protected override async Task Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(request.Id, cancellationToken); // order may be already in context
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
