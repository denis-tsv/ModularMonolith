using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Framework.UseCases.Interfaces.Exceptions;
using Shop.Order.DataAccess.Interfaces;

namespace Shop.Order.UseCases.Orders.Commands.DeleteOrder
{
    internal class DeleteOrderCommandHandler : AsyncRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderDbContext _dbContext;

        public DeleteOrderCommandHandler(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FindAsync(request.Id);
            
            if (order == null) throw new EntityNotFoundException();

            _dbContext.Orders.Remove(order);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}