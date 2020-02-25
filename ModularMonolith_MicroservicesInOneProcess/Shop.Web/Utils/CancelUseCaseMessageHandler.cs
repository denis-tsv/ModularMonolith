using MediatR;
using Shop.Utils.CancelUseCase;
using Shop.Utils.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Web.Utils
{
    public class CancelUseCaseMessageHandler<TMessage> : INotificationHandler<TMessage>
        where TMessage : ExceptionMessage
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICancelUseCaseService _cancelUseCaseService;

        public CancelUseCaseMessageHandler(IServiceProvider serviceProvider, ICancelUseCaseService cancelUseCaseService)
        {
            _serviceProvider = serviceProvider;
            _cancelUseCaseService = cancelUseCaseService;
        }
        public async Task Handle(TMessage message, CancellationToken cancellationToken)
        {
            if (_cancelUseCaseService.TryGet(message.CorrelationId, out var res))
            {
                var canceler = _serviceProvider.GetService(res.CancelerType);
                Task task = (Task) res.CancelerType.GetMethod("CancelAsync").Invoke(canceler, new object[] { res.Context });
                await task;
            }
        }
    }
}
