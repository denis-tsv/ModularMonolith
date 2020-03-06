using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Messaging
{
    public interface IMessageStore
    {
        Task AddAsync<TMessage>(TMessage message) where TMessage : Message;
        Task<List<TMessage>> AllAsync<TMessage>(string correlationId) where TMessage : Message;
        Task<List<Message>> AllAsync(string correlationId);
        Task<TMessage> SingleOrDefaultAsync<TMessage>(string correlationId) where TMessage : Message;
    }
}
