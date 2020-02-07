using System.Threading.Tasks;

namespace Shop.Utils.ServiceBus
{
    public interface IServiceBus
    {
        Task PublishAsync<T>(T message) where T : Message;
    }
}
