using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Messaging
{
    public interface IMessageStore
    {
        Task AddAsync<TMessage>(TMessage message) where TMessage : Message;
        Task<List<TMessage>> AllOfTypeAsync<TMessage>() where TMessage : Message;
        Task<List<Message>> AllAsync();
        Task<TMessage> SingleOrDefaultAsync<TMessage>() where TMessage : Message;
    }
}
