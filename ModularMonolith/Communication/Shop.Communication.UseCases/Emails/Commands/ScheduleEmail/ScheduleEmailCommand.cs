using MediatR;

namespace Shop.Communication.UseCases.Emails.Commands.ScheduleEmail
{
    internal class ScheduleEmailCommand : IRequest
    {
        public string Address { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
    }
}