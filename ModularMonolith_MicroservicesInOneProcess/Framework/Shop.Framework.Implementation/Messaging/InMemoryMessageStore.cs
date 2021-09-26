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
        
        public Task<TMessage> SingleOrDefaultAsync<TMessage>(string correlationId) where TMessage : Message
        {
            var result = _messages
                .OfType<TMessage>()
                .SingleOrDefault(x => x.CorrelationId == correlationId);
            return Task.FromResult(result);
        }
    }
}
