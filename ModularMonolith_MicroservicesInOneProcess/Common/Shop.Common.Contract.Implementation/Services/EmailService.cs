using System.Threading.Tasks;
using Shop.Common.Contract.Services;
using Shop.Common.Entities;
using Shop.Common.Infrastructure.Interfaces.DataAccess;
using Shop.Order.Contract.Orders;

namespace Shop.Common.Contract.Implementation.Services
{
    internal class EmailService : IEmailService
    {
        private readonly ICommonDbContext _dbContext;
        private readonly IOrderServiceContract _orderService;

        public EmailService(ICommonDbContext dbContext, IOrderServiceContract orderService)
        {
            _dbContext = dbContext;
            _orderService = orderService;
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
