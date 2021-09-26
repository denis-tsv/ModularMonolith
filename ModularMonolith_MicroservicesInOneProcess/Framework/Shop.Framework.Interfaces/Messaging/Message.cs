using MediatR;

namespace Shop.Framework.Interfaces.Messaging
{
    public abstract class Message : INotification
    {
        public string CorrelationId { get; set; }
    }
}
