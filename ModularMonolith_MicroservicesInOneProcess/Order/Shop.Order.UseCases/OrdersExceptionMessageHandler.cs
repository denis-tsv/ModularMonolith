using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Framework.Interfaces.Messaging;
using Shop.Order.Contract.Orders.Messages.CreateOrder;
using Shop.Order.UseCases.Orders.Commands.CancelOrder;

namespace Shop.Order.UseCases
{
    public class OrdersExceptionMessageHandler : INotificationHandler<ExceptionMessage>
    {
        private readonly IMessageStore _messageStore;
        private readonly IMessageBroker _messageBroker;

        public OrdersExceptionMessageHandler(IMessageStore messageStore, IMessageBroker messageBroker)
        {
            _messageStore = messageStore;
            _messageBroker = messageBroker;
        }
        public async Task Handle(ExceptionMessage message, CancellationToken cancellationToken)
        {
            var orderCreatedMessage= await _messageStore.SingleOrDefaultAsync<CreateOrderResponseMessage>(message.CorrelationId);

            if (orderCreatedMessage != null)
            {
                var cancelOrderMessage = new CancelOrderMesage
                {
                    CorrelationId = message.CorrelationId, 
                    OrderId = orderCreatedMessage.OrderId
                };
                await _messageBroker.PublishAsync(cancelOrderMessage);
            }
        }
    }
}
