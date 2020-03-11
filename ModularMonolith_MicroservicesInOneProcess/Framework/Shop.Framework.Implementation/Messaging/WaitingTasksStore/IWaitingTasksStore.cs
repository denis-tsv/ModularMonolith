using System;
using System.Threading.Tasks;
using Shop.Framework.Interfaces.Messaging;

namespace Shop.Framework.Implementation.Messaging.WaitingTasksStore
{
    internal interface IWaitingTasksStore
    {
        Task<TMessage> Add<TMessage>(Guid correlationId) where TMessage : Message;
        bool TryComplete<TMessage>(TMessage message) where TMessage : Message;
    }
}
