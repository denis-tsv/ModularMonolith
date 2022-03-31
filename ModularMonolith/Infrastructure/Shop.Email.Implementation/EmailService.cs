using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shop.Emails.Interfaces;

namespace Shop.Emails.Implementation
{
    internal class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly SmtpClient _client;

        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;

            _client = new SmtpClient(_emailOptions.Server, _emailOptions.Port);
            _client.Credentials = new NetworkCredential(_emailOptions.Email, _emailOptions.Password);
            _client.EnableSsl = true;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var from = new MailAddress(_emailOptions.Email, _emailOptions.FromName);
            var to = new MailAddress(email);
            var message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;

            _client.Send(message);
        }
    }
}