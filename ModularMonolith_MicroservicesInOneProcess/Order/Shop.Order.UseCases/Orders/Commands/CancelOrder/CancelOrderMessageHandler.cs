using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Commands.CancelOrder
{
    internal class CancelOrderMessageHandler : INotificationHandler<CancelOrderMesage>
    {
        private readonly IOrderDbContext _dbContext;

        public CancelOrderMessageHandler(IOrderDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CancelOrderMesage message, CancellationToken token)
        {
            //in real project online handler can schedule a background task to remove an order. in this demo project we don't use background jobs to make demo as simply as possible
            try
            {
                var order = await _dbContext.Orders.FindAsync(message.OrderId); //order already tracked by context
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //Log exception. Cancel handlers should not produce new exception messages because it can create infinite loop
            }
        }
    }
}
