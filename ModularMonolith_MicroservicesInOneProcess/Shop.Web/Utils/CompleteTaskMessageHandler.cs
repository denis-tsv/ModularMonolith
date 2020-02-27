using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Framework.Interfaces.Cancel;
using Shop.Framework.Interfaces.Messaging;
using Shop.Web.Utils.WaitingTasksStore;

namespace Shop.Web.Utils
{
    public class CompleteTaskMessageHandler<TMessage> : INotificationHandler<TMessage>
        where TMessage : Message
    {
        private readonly IWaitingTasksStore _waitingTasksStore;
        private readonly ICancelService _cancelService;
        private readonly IServiceProvider _serviceProvider;

        public CompleteTaskMessageHandler(IWaitingTasksStore waitingTasksStore, 
            ICancelService cancelService, IServiceProvider serviceProvider)
        {
            _waitingTasksStore = waitingTasksStore;
            _cancelService = cancelService;
            _serviceProvider = serviceProvider;
        }
        
        public async Task Handle(TMessage message, CancellationToken cancellationToken)
        {
            if (_waitingTasksStore.TryComplete(message))
            {
                if (_cancelService.TryRemoveCancel(message.CorrelationId, out var cancels))
                {
                    if (message is ExceptionMessage exceptionMessage)
                    {
                        var tasks = new List<Task>();
                        foreach (var cancel in cancels)
                        {
                            var cancelHandler = _serviceProvider.GetService(cancel.CancelHandlerType);
                            Task task = (Task)cancel.CancelHandlerType.GetMethod("HandleAsync").Invoke(cancelHandler, new object[] { cancel.Cancel });
                            tasks.Add(task);
                        }
                        
                        await Task.WhenAll(tasks);
                    }
                }
            }
        }
    }
}
