using System;
using System.Collections.Generic;
using System.Linq;
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
            var resTask = _waitingTasksStore.Add<TResultMessage>();

            await _messageBroker.PublishAsync(message);

            var result = await resTask;

            return result;
        }

        public async Task<Message[]> SendMessageAsync(Message message, Type[] resultMessageTypes)
        {
            if (resultMessageTypes == null) throw new ArgumentNullException(nameof(resultMessageTypes));
            if (!resultMessageTypes.Any()) throw new ArgumentException("resultMessageTypes array is empty");

            var tasks = new List<Task>();
            foreach (var messageType in resultMessageTypes)
            {
                var method = typeof(IWaitingTasksStore).GetMethod("Add").MakeGenericMethod(messageType);
                var task = method.Invoke(_waitingTasksStore, new object[]{});
                tasks.Add((Task)task);
            }

            await _messageBroker.PublishAsync(message);

            await Task.WhenAll(tasks);

            var result = tasks.Select(x =>
            {
                dynamic resTask = x;
                var message = resTask.Result;
                return (Message) message;
            }).ToArray();

            return result;
        }
    }
}
