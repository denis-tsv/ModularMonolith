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
            return Task.CompletedTask;
        }

        public Task<List<TMessage>> AllAsync<TMessage>(string correlationId) where TMessage : Message
        {
            var messageType = typeof(TMessage);
            var result = _messages
                .Where(x => x.CorrelationId == correlationId && x.GetType() == messageType)
                .Cast<TMessage>()
                .ToList();
            return Task.FromResult(result);
        }

        public Task<List<Message>> AllAsync(string correlationId)
        {
            var result = _messages
                .Where(x => x.CorrelationId == correlationId)
                .ToList();
            return Task.FromResult(result);
        }

        public Task<TMessage> SingleOrDefaultAsync<TMessage>(string correlationId) where TMessage : Message
        {
            var messageType = typeof(TMessage);
            var result = _messages
                .Where(x => x.CorrelationId == correlationId && x.GetType() == messageType)
                .Cast<TMessage>()
                .SingleOrDefault();
            return Task.FromResult(result);
        }
    }
}
