using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Common.Contract.Services;
using Shop.Order.Contract.Orders.Messages;
using Shop.Utils.ServiceBus;

namespace Shop.Common.Contract.Implementation
{
    internal class OrderCreatedMessageHandler : INotificationHandler<OrderCreatedMessage>
    {
        private readonly IEmailService _emailService;
        private readonly IServiceBus _serviceBus;

        public OrderCreatedMessageHandler(IEmailService emailService, IServiceBus serviceBus)
        {
            _emailService = emailService;
            _serviceBus = serviceBus;
        }
        public async Task Handle(OrderCreatedMessage message, CancellationToken cancellationToken)
        {
            try
            {
                //throw new Exception("Send email failed");

                await _emailService.SendEmailAsync(message.UserEmail, "Order created", $"Your order {message.OrderId} created successfully");

                await _serviceBus.PublishAsync(new FinishedOrderCreationMessage
                {
                    OrderId = message.OrderId,
                    CorrelationId = message.CorrelationId
                });
            }
            catch (Exception e)
            {
                // log exception using correlation id

                await _serviceBus.PublishAsync(new CancelOrderCreationMessage
                {
                    OrderId = message.OrderId,
                    CorrelationId = message.CorrelationId,
                    Exception = e
                });
            }
        }
    }
}
