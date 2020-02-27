using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Framework.Interfaces.Messaging;
using Shop.Web.Utils.WaitingTasksStore;

namespace Shop.Web.Utils
{
    public class CompleteTaskMessageHandler<TMessage> : INotificationHandler<TMessage>
        where TMessage : Message
    {
        private readonly IWaitingTasksStore _waitingTasksStore;

        public CompleteTaskMessageHandler(IWaitingTasksStore waitingTasksStore)
        {
            _waitingTasksStore = waitingTasksStore;
        }
        
        public Task Handle(TMessage message, CancellationToken cancellationToken)
        {
            _waitingTasksStore.TryComplete(message);
            return Task.CompletedTask;
        }
    }
}
