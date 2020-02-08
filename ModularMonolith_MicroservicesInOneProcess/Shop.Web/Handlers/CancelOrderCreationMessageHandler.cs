using MediatR;
using Shop.Order.Contract.Orders.Messages;
using Shop.Web.Utils.WaitingTasksStore;

namespace Shop.Web.Handlers
{
    public class CancelOrderCreationMessageHandler : NotificationHandler<CancelOrderCreationMessage>
    {
        private readonly IWaitingTasksStore _waitingTasksStore;

        public CancelOrderCreationMessageHandler(IWaitingTasksStore waitingTasksStore)
        {
            _waitingTasksStore = waitingTasksStore;
        }
        protected override void Handle(CancelOrderCreationMessage message)
        {
            _waitingTasksStore.CompleteException(message.CorrelationId, message.Exception);
        }
    }
}
