using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Common.Contract.Services;
using Shop.Order.Contract.Orders.Messages;
using Shop.Utils.ServiceBus;
using Shop.Utils.WaitingTasksStore;

namespace Shop.Common.Contract.Implementation
{
    internal class OrderCreatedMessageHandler : INotificationHandler<OrderCreatedMessage>
    {
        private readonly IEmailService _emailService;
        private readonly IServiceBus _serviceBus;
        private readonly IWaitingTasksStore _waitingTasksStore;

        public OrderCreatedMessageHandler(IEmailService emailService, IServiceBus serviceBus, IWaitingTasksStore waitingTasksStore)
        {
            _emailService = emailService;
            _serviceBus = serviceBus;
            _waitingTasksStore = waitingTasksStore;
        }
        public async Task Handle(OrderCreatedMessage message, CancellationToken cancellationToken)
        {
            try
            {
                throw new Exception("Send email failed");

                await _emailService.SendEmailAsync(message.UserEmail, "Order created", $"Your order {message.OrderId} created successfully");

                _waitingTasksStore.Complete(message.CorrelationId, message.OrderId);
            }
            catch (Exception e)
            {
                // log exception using correlation id

                await _serviceBus.PublishAsync(new CancelOrderCreationMessage
                {
                    OrderId = message.OrderId,
                    CorrelationId = message.CorrelationId
                });

                _waitingTasksStore.CompleteException(message.CorrelationId, e);
            }
        }
    }
}
