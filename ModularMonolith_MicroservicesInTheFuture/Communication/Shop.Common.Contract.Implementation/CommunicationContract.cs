using System;
using System.Threading.Tasks;
using MediatR;
using Shop.Communication.UseCases.Emails.Commands.SendEmail;
using Shop.Framework.Interfaces.Services;
using Shop.Order.Contract;

namespace Shop.Communication.Contract.Implementation
{
    internal class CommunicationContract : ICommunicationContract
    {
        private readonly IMediator _mediator;
        private readonly Lazy<IOrderContract> _orderContract;
        private readonly Lazy<IRequestContext> _requestContext;

        public CommunicationContract(IMediator mediator, Lazy<IOrderContract> orderContract, Lazy<IRequestContext> requestContext)
        {
            _mediator = mediator;
            _orderContract = orderContract;
            _requestContext = requestContext;
        }

        public async Task SendEmailAsync(string email, string subject, string body, int orderId)
        {
            await _mediator.Send(new SendEmailRequest
            {
                Address = email,
                Subject = subject,
                Body = body,
                OrderId = orderId
            });
        }

        public async Task SendOrderEmailRequestContextAsync(string email, string subject, string body)
        {
            try
            {
                await _mediator.Send(new SendEmailRequest
                {
                    Address = email,
                    Subject = subject,
                    Body = body
                });
            }
            catch
            {
                var orderId = _requestContext.Value.GetValue<int>(OrderRequestContextKeys.OrderId);
                await _orderContract.Value.CancelCreateOrderAsync(orderId);

                throw; //API client will receive OK status without throw 
            }
        }

        public async Task SendOrderEmailAsync(int orderId, string email, string subject, string body)
        {
            try
            {
                await _mediator.Send(new SendEmailRequest
                {
                    Address = email,
                    Subject = subject,
                    Body = body
                }); 
            }
            catch
            {
                await _orderContract.Value.CancelCreateOrderAsync(orderId);

                throw; //API client will receive OK status without throw 
            }
        }
    }
}
