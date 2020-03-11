using System;
using MediatR;

namespace Shop.Framework.Interfaces.Messaging
{
    public abstract class Message : INotification
    {
        public Guid CorrelationId { get; set; }
    }
}
