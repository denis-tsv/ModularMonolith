using MediatR;
using Shop.Order.Contract.Orders.Messages;
using Shop.Web.Utils.WaitingTasksStore;

namespace Shop.Web.Handlers
{
    public class FinishedOrderCreationMessageHandler : NotificationHandler<FinishedOrderCreationMessage>
    {
        private readonly IWaitingTasksStore _waitingTasksStore;

        public FinishedOrderCreationMessageHandler(IWaitingTasksStore waitingTasksStore)
        {
            _waitingTasksStore = waitingTasksStore;
        }
        protected override void Handle(FinishedOrderCreationMessage message)
        {
            _waitingTasksStore.Complete(message.CorrelationId, message.OrderId);
        }
    }
}
