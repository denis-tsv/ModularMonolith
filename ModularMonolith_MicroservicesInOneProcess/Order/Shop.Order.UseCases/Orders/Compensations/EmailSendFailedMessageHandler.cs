using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Communication.Contract.Messages;
using Shop.Framework.Interfaces.Messaging;
using Shop.Order.Contract.Orders.Messages.CreateOrder;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Compensations
{
    internal class EmailSendFailedMessageHandler : INotificationHandler<EmailSendFailedMessage>
    {
        private readonly IMessageStore _messageStore;
        private readonly IOrderDbContext _dbContext;

        public EmailSendFailedMessageHandler(IMessageStore messageStore, IOrderDbContext dbContext)
        {
            _messageStore = messageStore;
            _dbContext = dbContext;
        }
        public async Task Handle(EmailSendFailedMessage message, CancellationToken token)
        {
            var orderCreatedMessage = await _messageStore.SingleOrDefaultAsync<CreateOrderResponseMessage>(message.CorrelationId);
            if (orderCreatedMessage == null) return;

            //in real project online handler can schedule a background task to remove an order. in this demo project we don't use background jobs to make demo as simply as possible
            try
            {
                var order = await _dbContext.Orders.FindAsync(orderCreatedMessage.OrderId, token); //order already tracked by context
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync(token);
            }
            catch (Exception e)
            {
                //Log exception. Cancel handlers should not produce new exception messages because it can create infinite loop
            }
        }
    }
}
