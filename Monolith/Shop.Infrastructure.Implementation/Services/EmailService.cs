using System.Threading.Tasks;
using Shop.Entities;
using Shop.Infrastructure.Interfaces.DataAccess;
using Shop.Infrastructure.Interfaces.Services;

namespace Shop.Infrastructure.Implementation.Services
{
    public class EmailService : IEmailService
    {
        private readonly IDbContext _dbContext;
        
        public EmailService(IDbContext dbContext)
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
