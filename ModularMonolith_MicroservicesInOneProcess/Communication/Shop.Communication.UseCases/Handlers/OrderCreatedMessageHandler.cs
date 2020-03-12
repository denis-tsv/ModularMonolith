using System.Threading.Tasks;
using Shop.Communication.Contract.Messages;
using Shop.Communication.Infrastructure.Interfaces.Services;
using Shop.Framework.Interfaces.Messaging;
using Shop.Framework.Interfaces.Services;
using Shop.Order.Contract.Orders.Messages.CreateOrder;

namespace Shop.Communication.UseCases.Handlers
{
    internal class OrderCreatedMessageHandler : MessageHandler<CreateOrderResponseMessage>
    {
        private readonly IEmailService _emailService;
        private readonly IUrlHelper _urlHelper;
        private readonly ICurrentUserService _currentUserService;

        public OrderCreatedMessageHandler(IEmailService emailService, IMessageBroker messageBroker, IUrlHelper urlHelper, ICurrentUserService currentUserService) : 
            base(messageBroker)
        {
            _emailService = emailService;
            _urlHelper = urlHelper;
            _currentUserService = currentUserService;
        }
        protected override async Task Handle(CreateOrderResponseMessage message)
        {
            //TODO fix _urlHelper because it returns null
            var orderDetailsUrl = _urlHelper.Action("get", "/api/orders", new object[]{message.OrderId.ToString()}, "http");
            await _emailService.SendEmailAsync(_currentUserService.Email, "Order created", $"Your order {message.OrderId} created successfully. You can see details using link {orderDetailsUrl}");

            await MessageBroker.PublishAsync(new UserEmailNotifiedMessage());
        }
    }
}
