using System;
using System.Threading;
using System.Threading.Tasks;
using Shop.Communication.DataAccess.Interfaces;
using Shop.Communication.Entities;
using Shop.Framework.UseCases.Interfaces.Services;

namespace Shop.Communication.Contract.Implementation
{
    internal class CommunicationContract : ICommunicationContract
    {
        private readonly ICommunicationDbContext _communicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public CommunicationContract(ICommunicationDbContext communicationDbContext, ICurrentUserService currentUserService)
        {
            _communicationDbContext = communicationDbContext;
            _currentUserService = currentUserService;
        }

        public async Task ScheduleOrderCreatedEmailAsync(string email, int orderId, CancellationToken cancellationToken)
        {
            var mail = new Email
            {
                Address = email,
                Subject = "Order created",
                Body = $"Your order {orderId} created successfully",
                OrderId = orderId,
                UserId = _currentUserService.Id
            };
            _communicationDbContext.Emails.Add(mail);
            await _communicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
