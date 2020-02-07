using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Order.Contract.Orders.Messages;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Commands.CancelCreateOrder
{
    internal class CancelOrderCreationMessageHandler : INotificationHandler<CancelOrderCreationMessage>
    {
        private readonly IOrderDbContext _dbContext;

        public CancelOrderCreationMessageHandler(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CancelOrderCreationMessage message, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _dbContext.Orders.FindAsync(message.OrderId); //order already tracked by context
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                //log using correlation id
                //schedule task to remove order
            }
        }
    }
}
