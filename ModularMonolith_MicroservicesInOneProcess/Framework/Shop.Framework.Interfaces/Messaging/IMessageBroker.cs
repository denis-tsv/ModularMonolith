using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Messaging
{
    //Only send message
    public interface IMessageBroker
    {
        Task PublishAsync<T>(T message) where T : Message;
    }
}
