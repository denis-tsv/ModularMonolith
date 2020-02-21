using System;
using System.Threading.Tasks;
using Shop.Common.Contract.Services;
using Shop.Common.Infrastructure.Interfaces.Services;
using Shop.Order.Contract;
using Shop.Order.Contract.Orders;
using Shop.Utils.Sagas;

namespace Shop.Common.Contract.Implementation.Services
{
    internal class EmailServiceContract : IEmailServiceContract
    {
        private readonly IEmailService _emailService;
        private readonly ISaga _saga;

        //this class will contain a lot of dependencies and a lot of methods. One dependency and one method for each operation
        //it is better to use Lazy when class contains a lot of many dependencies but use only one of them
        private readonly Lazy<IOrderServiceContract> _orderService;
        //private readonly Lazy<IUserServiceContract> _userService; // check user's email, password recovery and etc.

        public EmailServiceContract(IEmailService emailService, ISaga saga, Lazy<IOrderServiceContract> orderService)
        {
            _emailService = emailService;
            _saga = saga;
            _orderService = orderService;
        }

        public async Task SendOrderEmailSagaAsync(string email, string subject, string body)
        {
            try
            {
                await _emailService.SendEmailAsync(email, subject, body);
            }
            catch (Exception e)
            {
                var orderId = _saga.GetValue<int>(OrderSagaKeys.OrderId);
                await _orderService.Value.CancelCreateOrderAsync(orderId);

                throw; //API client will receive OK status without throw 
            }
        }

        public async Task SendOrderEmailAsync(int orderId, string email, string subject, string body)
        {
            try
            {
                await _emailService.SendEmailAsync(email, subject, body);
            }
            catch (Exception e)
            {
                await _orderService.Value.CancelCreateOrderAsync(orderId);

                throw; //API client will receive OK status without throw 
            }
        }
    }
}
