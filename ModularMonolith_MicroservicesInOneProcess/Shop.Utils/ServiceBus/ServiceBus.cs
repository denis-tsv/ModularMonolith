using System.Threading.Tasks;
using MediatR;

namespace Shop.Utils.ServiceBus
{
    public class ServiceBus : IServiceBus
    {
        private readonly IMediator _mediator;

        public ServiceBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishAsync<T>(T message) where T : Message
        {
            //Some modules can stay in the same process and some modules could be converted to microservices
            //So we can use Mediatr to send messages for modules in the same process and something like Azure Service Bus to send messages for modules in other processes

            //if (typeof(T).Name == "MessageForRemoteModule")
            //    return _azureServiceBus.SendAsync();
            //else
            //     return _mediator.Publish(message);

            return _mediator.Publish(message);
        }
    }
}
