using System;
using System.Threading.Tasks;
using Shop.Framework.Interfaces.Messaging;
using Shop.Identity.Contract.Identity.Dto;
using Shop.Identity.Contract.Identity.UserDetails;

namespace Shop.Identity.UseCases.Identity.Queries.UserDetails
{
    public class UserDetailsRequestMessageHandler : MessageHandler<UserDetailsRequestMessage>
    {
        public UserDetailsRequestMessageHandler(IMessageBroker messageBroker) : base(messageBroker)
        {
        }

        protected override async Task Handle(UserDetailsRequestMessage message)
        {
            DateTimeOffset? lockoutEnd = null;// DateTimeOffset.MaxValue;

            var response = new UserDetailsResponseMessage
            {
                CorrelationId = message.CorrelationId,
                UserDetailsDto = new UserDetailsDto { LockoutEnd  = lockoutEnd}
            }; 
            await MessageBroker.PublishAsync(response);
        }
    }
}
