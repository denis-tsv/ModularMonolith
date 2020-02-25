using System.Threading.Tasks;
using Shop.Common.Entities;
using Shop.Common.Infrastructure.Interfaces.DataAccess;
using Shop.Common.Infrastructure.Interfaces.Services;

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
            //throw new Exception("Something wrong with send email");

            await _dbContext.SaveChangesAsync();
        }
    }
}
