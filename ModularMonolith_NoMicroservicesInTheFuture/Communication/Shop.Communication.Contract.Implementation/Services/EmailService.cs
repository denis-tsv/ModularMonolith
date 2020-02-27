using System.Threading.Tasks;
using Shop.Communication.Contract.Services;
using Shop.Communication.Entities;
using Shop.Communication.Infrastructure.Interfaces.DataAccess;

namespace Shop.Communication.Contract.Implementation.Services
{
    internal class EmailService : IEmailService
    {
        private readonly ICommunicationDbContext _dbContext;
        
        public EmailService(ICommunicationDbContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var newMail = new Email
            {
                Address = email,
                Subject = subject,
                Body = body
            };

            _dbContext.Emails.Add(newMail);

            await _dbContext.SaveChangesAsync();
        }
    }
}
