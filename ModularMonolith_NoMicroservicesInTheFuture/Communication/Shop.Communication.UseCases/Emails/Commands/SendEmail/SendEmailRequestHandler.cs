using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Communication.Entities;
using Shop.Communication.Infrastructure.Interfaces.DataAccess;

namespace Shop.Communication.UseCases.Emails.Commands.SendEmail
{
    internal class SendEmailRequestHandler : AsyncRequestHandler<SendEmailRequest>
    {
        private readonly ICommunicationDbContext _dbContext;

        public SendEmailRequestHandler(ICommunicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected override async Task Handle(SendEmailRequest request, CancellationToken cancellationToken)
        {
            var newMail = new Email
            {
                Address = request.Address,
                Subject = request.Subject,
                Body = request.Body
            };

            _dbContext.Emails.Add(newMail);

            await _dbContext.SaveChangesAsync();
        }
    }
}
