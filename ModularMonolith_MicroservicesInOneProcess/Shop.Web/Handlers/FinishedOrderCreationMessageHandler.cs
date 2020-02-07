using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Order.Contract.Orders.Messages;
using Shop.Web.Utils.WaitingTasksStore;

namespace Shop.Web.Handlers
{
    public class FinishedOrderCreationMessageHandler : INotificationHandler<FinishedOrderCreationMessage>
    {
        private readonly IWaitingTasksStore _waitingTasksStore;

        public FinishedOrderCreationMessageHandler(IWaitingTasksStore waitingTasksStore)
        {
            _waitingTasksStore = waitingTasksStore;
        }
        public Task Handle(FinishedOrderCreationMessage message, CancellationToken cancellationToken)
        {
            _waitingTasksStore.Complete(message.CorrelationId, message.OrderId);

            return Task.CompletedTask;
        }
    }
}
