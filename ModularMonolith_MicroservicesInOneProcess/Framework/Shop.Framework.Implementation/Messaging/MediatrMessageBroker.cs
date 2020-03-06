using System;
using System.Threading.Tasks;
using MediatR;
using Shop.Framework.Interfaces.Messaging;
using Shop.Utils.Extensions;

namespace Shop.Framework.Implementation.Messaging
{
    internal class MediatrMessageBroker : IMessageBroker
    {
        private readonly IMediator _mediator;
        private readonly IMessageStore _messageStore;

        public MediatrMessageBroker(IMediator mediator, IMessageStore messageStore)
        {
            _mediator = mediator;
            _messageStore = messageStore;
        }

        public async Task PublishAsync<T>(T message) where T : Message
        {
            if (message.CorrelationId.IsEmpty()) throw new ArgumentException("message.CorrelationId is null");

            await _messageStore.AddAsync(message);

            await _mediator.Publish(message);
        }
    }
}
