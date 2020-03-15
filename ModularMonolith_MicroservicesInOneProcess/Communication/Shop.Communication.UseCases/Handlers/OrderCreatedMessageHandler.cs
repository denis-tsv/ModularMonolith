using System.Threading.Tasks;
using Shop.Communication.Contract.Messages;
using Shop.Communication.Entities;
using Shop.Communication.Infrastructure.Interfaces.DataAccess;
using Shop.Framework.Interfaces.Messaging;
using Shop.Framework.Interfaces.Services;
using Shop.Order.Contract.Orders.Messages.CreateOrder;

namespace Shop.Communication.UseCases.Handlers
{
    internal class OrderCreatedMessageHandler : MessageHandler<CreateOrderResponseMessage>
    {
        private readonly ICommunicationDbContext _dbContext;
        private readonly IUrlHelper _urlHelper;
        private readonly ICurrentUserService _currentUserService;

        public OrderCreatedMessageHandler(ICommunicationDbContext dbContext, IMessageBroker messageBroker, IUrlHelper urlHelper, ICurrentUserService currentUserService) : 
            base(messageBroker)
        {
            _dbContext = dbContext;
            _urlHelper = urlHelper;
            _currentUserService = currentUserService;
        }
        protected override async Task Handle(CreateOrderResponseMessage message)
        {
            //TODO fix _urlHelper because it returns null
            var orderDetailsUrl = _urlHelper.Action("get", "/api/orders", new object[] { message.OrderId.ToString() }, "http");

            var newEmail = new Email
            {
                Address = _currentUserService.Email,
                Subject = "Order created",
                Body = $"Your order {message.OrderId} created successfully. You can see details using link {orderDetailsUrl}",
                OrderId = message.OrderId,
                UserId = _currentUserService.Id
            };

            _dbContext.Emails.Add(newEmail);
            await _dbContext.SaveChangesAsync();

            await MessageBroker.PublishAsync(new UserEmailNotifiedMessage());
        }
    }
}
