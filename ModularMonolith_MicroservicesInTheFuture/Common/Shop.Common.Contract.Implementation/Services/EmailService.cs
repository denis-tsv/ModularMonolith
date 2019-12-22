using System;
using System.Threading.Tasks;
using Shop.Common.Contract.Services;
using Shop.Common.Entities;
using Shop.Common.Infrastructure.Interfaces.DataAccess;
using Shop.Order.Contract;
using Shop.Order.Contract.Orders;
using Shop.Utils.Sagas;

namespace Shop.Common.Contract.Implementation.Services
{
    internal class EmailService : IEmailService
    {
        private readonly ICommonDbContext _dbContext;
        private readonly ISaga _saga;
        private readonly IOrderServiceContract _orderService;

        public EmailService(ICommonDbContext dbContext, ISaga saga, IOrderServiceContract orderService)
        {
            _dbContext = dbContext;
            _saga = saga;
            _orderService = orderService;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
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
            catch (Exception e)
            {
                //TODO log exception
                var orderId = _saga.GetValue<int>(OrderSagaKeys.OrderId);
                await _orderService.CancelCreateOrderAsync(orderId);

                throw; //API client will receive OK status without throw 
            }
        }
    }
}
