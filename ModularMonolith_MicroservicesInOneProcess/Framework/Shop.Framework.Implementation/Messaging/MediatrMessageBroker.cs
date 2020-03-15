using System.Threading.Tasks;
using MediatR;
using Shop.Framework.Implementation.Messaging.WaitingTasksStore;
using Shop.Framework.Interfaces.Messaging;

namespace Shop.Framework.Implementation.Messaging
{
    internal class MediatrMessageBroker : IMessageBroker
    {
        private readonly IMediator _mediator;
        private readonly IMessageStore _messageStore;
        private readonly IWaitingTasksStore _waitingTasksStore;

        public MediatrMessageBroker(IMediator mediator, IMessageStore messageStore, IWaitingTasksStore waitingTasksStore)
        {
            _mediator = mediator;
            _messageStore = messageStore;
            _waitingTasksStore = waitingTasksStore;
        }

        public async Task PublishAsync<T>(T message) where T : Message
        {
            await _messageStore.AddAsync(message);

            await _mediator.Publish(message);

            _waitingTasksStore.TryComplete(message);
        }
    }
}
