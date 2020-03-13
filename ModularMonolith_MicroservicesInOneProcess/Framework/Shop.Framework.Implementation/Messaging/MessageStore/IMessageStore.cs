using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Framework.Interfaces.Messaging;

namespace Shop.Framework.Implementation.Messaging.MessageStore
{
    public interface IMessageStore
    {
        Task AddAsync<TMessage>(TMessage message) where TMessage : Message;
        Task<List<TMessage>> AllOfTypeAsync<TMessage>() where TMessage : Message;
        Task<List<Message>> AllAsync();
        Task<TMessage> SingleOrDefaultAsync<TMessage>() where TMessage : Message;
    }
}
