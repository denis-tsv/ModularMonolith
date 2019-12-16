using System.Threading.Tasks;
using Shop.Common.Contract.Services;
using Shop.Common.Entities;
using Shop.Common.Infrastructure.Interfaces.DataAccess;

namespace Shop.Common.Infrastructure.Implementation.Services
{
    internal class EmailService : IEmailService
    {
        private readonly ICommonDbContext _dbContext;

        public EmailService(ICommonDbContext dbContext)
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
