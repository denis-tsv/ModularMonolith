using System.Threading.Tasks;
using MediatR;
using Shop.Communication.UseCases.Emails.Commands.SendEmail;

namespace Shop.Communication.Contract.Implementation
{
    internal class CommunicationContract : ICommunicationContract
    {
        private readonly IMediator _mediator;

        public CommunicationContract(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            await _mediator.Send(new SendEmailRequest
            {
                Address = email,
                Subject = subject,
                Body = body
            });
        }
    }
}
