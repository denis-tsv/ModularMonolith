using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Commands.CancelOrder
{
    internal class CancelOrderRequestHandler : AsyncRequestHandler<CancelOrderRequest>
    {
        private readonly IOrderDbContext _dbContext;

        public CancelOrderRequestHandler(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(CancelOrderRequest request, CancellationToken cancellationToken)
        {
            //in real project online handler can schedule a background task to remove an order. in this demo project we don't use background jobs to make demo as simply as possible
            try
            {
                var order = await _dbContext.Orders.FindAsync(request.Id); //order already tracked by context
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                //log
                //schedule task to remove order
            }
        }
    }
}
