using System;
using System.Threading.Tasks;
using Shop.Framework.Implementation.Messaging.WaitingTasksStore;
using Shop.Framework.Interfaces.Messaging;

namespace Shop.Framework.Implementation.Messaging
{
    internal class MessageDispatcher : IMessageDispatcher
    {
        private readonly IMessageBroker _messageBroker;
        private readonly IWaitingTasksStore _waitingTasksStore;

        public MessageDispatcher(IMessageBroker messageBroker, IWaitingTasksStore waitingTasksStore)
        {
            _messageBroker = messageBroker;
            _waitingTasksStore = waitingTasksStore;
        }

        public  async Task<TResultMessage> SendMessageAsync<TResultMessage>(Message message) where TResultMessage : Message
        {
            message.CorrelationId = Guid.NewGuid().ToString();
            var resTask = _waitingTasksStore.Add<TResultMessage>(message.CorrelationId);

            await _messageBroker.PublishAsync(message);

            var result = await resTask;

            return result;
        }
    }
}
