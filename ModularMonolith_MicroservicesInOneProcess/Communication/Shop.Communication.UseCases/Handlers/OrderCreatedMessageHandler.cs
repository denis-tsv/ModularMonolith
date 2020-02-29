using System.Threading.Tasks;
using Shop.Communication.Contract.Messages;
using Shop.Communication.Infrastructure.Interfaces.Services;
using Shop.Framework.Interfaces.Messaging;
using Shop.Framework.Interfaces.Services;
using Shop.Order.Contract.Orders.Messages.CreateOrder;

namespace Shop.Communication.UseCases.Handlers
{
    internal class OrderCreatedMessageHandler : MessageHandler<OrderCreatedMessage>
    {
        private readonly IEmailService _emailService;
        private readonly IUrlHelper _urlHelper;

        public OrderCreatedMessageHandler(IEmailService emailService, IMessageBroker messageBroker, IUrlHelper urlHelper) : 
            base(messageBroker)
        {
            _emailService = emailService;
            _urlHelper = urlHelper;
        }
        protected override async Task Handle(OrderCreatedMessage message)
        {
            //TODO fix _urlHelper because it returns null
            var orderDetailsUrl = _urlHelper.Action("get", "/api/orders", new object[]{message.OrderId.ToString()}, "http");
            await _emailService.SendEmailAsync(message.UserEmail, "Order created", $"Your order {message.OrderId} created successfully. You can see details using link {orderDetailsUrl}");

            await MessageBroker.PublishAsync(new UserEmailNotifiedMessage
            {
                OrderId = message.OrderId,
                CorrelationId = message.CorrelationId
            });
        }
    }
}
