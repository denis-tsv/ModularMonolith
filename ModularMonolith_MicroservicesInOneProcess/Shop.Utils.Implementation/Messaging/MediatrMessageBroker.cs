using System.Threading.Tasks;
using MediatR;
using Shop.Utils.Messaging;

namespace Shop.Utils.Implementation.Messaging
{
    public class MediatrMessageBroker : IMessageBroker
    {
        private readonly IMediator _mediator;

        public MediatrMessageBroker(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishAsync<T>(T message) where T : Message
        {
            return _mediator.Publish(message);
        }
    }
}
