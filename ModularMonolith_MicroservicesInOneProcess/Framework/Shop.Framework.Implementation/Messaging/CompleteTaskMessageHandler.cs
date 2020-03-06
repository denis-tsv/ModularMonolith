using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Framework.Implementation.Messaging.WaitingTasksStore;
using Shop.Framework.Interfaces.Cancel;
using Shop.Framework.Interfaces.Messaging;

namespace Shop.Framework.Implementation.Messaging
{
    internal class CompleteTaskMessageHandler<TMessage> : INotificationHandler<TMessage>
        where TMessage : Message
    {
        private readonly IWaitingTasksStore _waitingTasksStore;
        private readonly ICancelService _cancelService;

        public CompleteTaskMessageHandler(IWaitingTasksStore waitingTasksStore, 
            ICancelService cancelService)
        {
            _waitingTasksStore = waitingTasksStore;
            _cancelService = cancelService;
        }
        
        public async Task Handle(TMessage message, CancellationToken cancellationToken)
        {
            if (_waitingTasksStore.TryComplete(message))
            {
                //if (message is ExceptionMessage exceptionMessage)
                //{
                //    await _cancelService.CancelAllAsync(message.CorrelationId);
                //}
                //else
                //{
                //    _cancelService.RemoveAll(message.CorrelationId);
                //}
            }
        }
    }
}
