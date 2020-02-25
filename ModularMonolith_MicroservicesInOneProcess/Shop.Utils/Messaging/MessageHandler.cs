using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Utils.Messaging
{
    public abstract class MessageHandler<TMessage> : INotificationHandler<TMessage>
        where TMessage : Message
    {
        protected readonly IMessageBroker MessageBroker;

        protected MessageHandler(IMessageBroker messageBroker)
        {
            MessageBroker = messageBroker;
        }

        protected abstract Task Handle(TMessage message);        

        async Task INotificationHandler<TMessage>.Handle(TMessage message, CancellationToken cancellationToken)
        {
            try
            {
                await Handle(message);
            }
            catch (Exception e)
            {
                await MessageBroker.PublishAsync(new ExceptionMessage { CorrelationId = message.CorrelationId, Exception = e });
            }
        }
    }
}
