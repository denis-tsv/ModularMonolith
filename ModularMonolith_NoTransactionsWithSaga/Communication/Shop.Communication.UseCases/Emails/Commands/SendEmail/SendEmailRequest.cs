using MediatR;

namespace Shop.Communication.UseCases.Emails.Commands.SendEmail
{
    internal class SendEmailRequest : IRequest
    {
        public string Address { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int OrderId { get; set; }
    }
}
