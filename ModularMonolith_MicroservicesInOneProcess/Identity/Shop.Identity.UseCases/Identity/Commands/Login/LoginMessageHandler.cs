using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Framework.Interfaces.Exceptions;
using Shop.Framework.Interfaces.Messaging;
using Shop.Identity.Contract.Identity;
using Shop.Identity.Contract.Identity.Login;
using Shop.Identity.Infrastructure.Interfaces.DataAccess;

namespace Shop.Identity.UseCases.Identity.Commands.Login
{
    internal class LoginMessageHandler : MessageHandler<LoginRequestMessage>
    {
        private readonly IIdentityDbContext _dbContext;

        public LoginMessageHandler(IIdentityDbContext dbContext, IMessageBroker messageBroker) : base(messageBroker)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(LoginRequestMessage message)
        {
            var user = await _dbContext.Users.AsNoTracking()
                .SingleOrDefaultAsync(x => x.NormalizedEmail == message.LoginDto.Email.ToUpper());
            if (user == null) throw new EntityNotFoundException();

            await MessageBroker.PublishAsync(new LoginResponseMessage {CorrelationId = message.CorrelationId});
        }
    }
}
