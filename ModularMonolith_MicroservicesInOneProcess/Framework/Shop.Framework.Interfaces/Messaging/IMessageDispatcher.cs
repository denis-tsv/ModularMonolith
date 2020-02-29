using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Messaging
{
    //Send message and wait for another message with the same correlation id
    public interface IMessageDispatcher
    {
        Task<TResultMessage> SendMessageAsync<TResultMessage>(Message message) where TResultMessage : Message;
    }
}
