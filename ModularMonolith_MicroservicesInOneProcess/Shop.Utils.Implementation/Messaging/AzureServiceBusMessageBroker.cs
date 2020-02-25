using System;
using System.Threading.Tasks;
using Shop.Utils.Messaging;

namespace Shop.Utils.Implementation.Messaging
{
    public class AzureServiceBusMessageBroker : IMessageBroker
    {
        public Task PublishAsync<T>(T message) where T : Message
        {
            throw new NotImplementedException();
        }
    }
}
