using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Communication.Contract.Messages;
using Shop.Communication.Infrastructure.Interfaces.Services;
using Shop.Framework.Interfaces.Messaging;
using Shop.Framework.Interfaces.Services;
using Shop.Order.Contract.Orders.Messages.CreateOrder;

namespace Shop.Communication.Contract.Implementation
{
    internal class OrderCreatedMessageHandler : INotificationHandler<OrderCreatedMessage>
    {
        private readonly IEmailService _emailService;
        private readonly IMessageBroker _messageBroker;
        private readonly IUrlHelper _urlHelper;

        public OrderCreatedMessageHandler(IEmailService emailService, IMessageBroker messageBroker, IUrlHelper urlHelper)
        {
            _emailService = emailService;
            _messageBroker = messageBroker;
            _urlHelper = urlHelper;
        }
        public async Task Handle(OrderCreatedMessage message, CancellationToken cancellationToken)
        {
            try
            {
                //TODO fix _urlHelper because it returns null
                var orderDetailsUrl = _urlHelper.Action("get", "/api/orders", new object[]{message.OrderId.ToString()}, "http");
                await _emailService.SendEmailAsync(message.UserEmail, "Order created", $"Your order {message.OrderId} created successfully. You can see details using link {orderDetailsUrl}");

                await _messageBroker.PublishAsync(new EntityEmailMessage
                {
                    Id = message.OrderId,
                    CorrelationId = message.CorrelationId
                });
            }
            catch (Exception e)
            {
                // log exception using correlation id

                await _messageBroker.PublishAsync(new ExceptionMessage
                {
                    CorrelationId = message.CorrelationId,
                    Exception = e
                });
            }
        }
    }
}
