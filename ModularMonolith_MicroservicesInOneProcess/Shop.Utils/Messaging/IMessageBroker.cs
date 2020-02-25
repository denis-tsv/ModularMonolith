using System.Threading.Tasks;

namespace Shop.Utils.Messaging
{
    public interface IMessageBroker
    {
        Task PublishAsync<T>(T message) where T : Message;
    }
}
