using MediatR;

namespace Shop.Utils.Messaging
{
    public abstract class Message : INotification
    {
        public string CorrelationId { get; set; }
    }
}
