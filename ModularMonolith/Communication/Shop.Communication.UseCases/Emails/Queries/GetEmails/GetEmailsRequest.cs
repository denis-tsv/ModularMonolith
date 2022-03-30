using MediatR;
using Shop.Communication.UseCases.Emails.Dto;

namespace Shop.Communication.UseCases.Emails.Queries.GetEmails
{
    internal class GetEmailsRequest : IRequest<EmailDto[]>, ICommunicationRequest
    {
    }
}
