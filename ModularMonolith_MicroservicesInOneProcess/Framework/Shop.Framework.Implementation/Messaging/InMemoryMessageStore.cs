using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Framework.Interfaces.Messaging;

namespace Shop.Framework.Implementation.Messaging
{
    internal class InMemoryMessageStore : IMessageStore
    {
        private readonly List<Message> _messages = new List<Message>();

        public Task AddAsync<TMessage>(TMessage message) where TMessage : Message
        {
            _messages.Add(message);

            //TODO save message in store with columns: DateTime, MessageType, CorrelationId, MessageData (serialized message)

            return Task.CompletedTask;
        }

        public Task<List<TMessage>> AllOfTypeAsync<TMessage>() where TMessage : Message
        {
            var messageType = typeof(TMessage);
            var result = _messages
                .Where(x => x.GetType() == messageType)
                .Cast<TMessage>()
                .ToList();
            return Task.FromResult(result);
        }

        public Task<List<Message>> AllAsync()
        {
            var result = _messages.ToList();
            return Task.FromResult(result);
        }

        public Task<TMessage> SingleOrDefaultAsync<TMessage>() where TMessage : Message
        {
            var result = _messages
                .OfType<TMessage>()
                .SingleOrDefault();
            return Task.FromResult(result);
        }
    }
}
