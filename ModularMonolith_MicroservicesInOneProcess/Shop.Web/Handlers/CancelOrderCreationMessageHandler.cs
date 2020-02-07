using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Order.Contract.Orders.Messages;
using Shop.Web.Utils.WaitingTasksStore;

namespace Shop.Web.Handlers
{
    public class CancelOrderCreationMessageHandler : INotificationHandler<CancelOrderCreationMessage>
    {
        private readonly IWaitingTasksStore _waitingTasksStore;

        public CancelOrderCreationMessageHandler(IWaitingTasksStore waitingTasksStore)
        {
            _waitingTasksStore = waitingTasksStore;
        }
        public Task Handle(CancelOrderCreationMessage message, CancellationToken cancellationToken)
        {
            _waitingTasksStore.CompleteException(message.CorrelationId, message.Exception);
            return Task.CompletedTask;
        }
    }
}
