using System;
using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Messaging
{
    public interface IMessageDispatcher
    {
        //Send message and wait for another message with specified type
        Task<TResultMessage> SendMessageAsync<TResultMessage>(Message message) where TResultMessage : Message;

        Task<Message[]> SendMessageAsync(Message message, Type[] resultMessageTypes);
    }
}
