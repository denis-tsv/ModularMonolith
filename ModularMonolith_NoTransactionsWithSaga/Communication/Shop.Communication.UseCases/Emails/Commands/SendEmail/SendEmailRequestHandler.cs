using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Communication.Entities;
using Shop.Communication.Infrastructure.Interfaces.DataAccess;
using Shop.Framework.Interfaces.Services;

namespace Shop.Communication.UseCases.Emails.Commands.SendEmail
{
    internal class SendEmailRequestHandler : AsyncRequestHandler<SendEmailRequest>
    {
        private readonly ICommunicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public SendEmailRequestHandler(ICommunicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }
        protected override async Task Handle(SendEmailRequest request, CancellationToken cancellationToken)
        {
            var newMail = new Email
            {
                Address = request.Address,
                Subject = request.Subject,
                Body = request.Body,
                UserId = _currentUserService.Id,
                OrderId = request.OrderId
            };

            _dbContext.Emails.Add(newMail);
            //throw new System.Exception("Email error");
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
