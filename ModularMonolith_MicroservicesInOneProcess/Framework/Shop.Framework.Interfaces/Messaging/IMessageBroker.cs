using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Messaging
{
    public interface IMessageBroker
    {
        Task PublishAsync<T>(T message) where T : Message;
    }
}
