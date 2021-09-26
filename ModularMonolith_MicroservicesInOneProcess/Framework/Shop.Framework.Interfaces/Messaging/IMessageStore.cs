using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Messaging
{
    public interface IMessageStore
    {
        Task AddAsync<TMessage>(TMessage message) where TMessage : Message;
        Task<TMessage> SingleOrDefaultAsync<TMessage>(string correlationId) where TMessage : Message;
    }
}
