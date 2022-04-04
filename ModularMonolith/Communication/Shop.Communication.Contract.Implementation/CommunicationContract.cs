using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Communication.DataAccess.Interfaces;

namespace Shop.Communication.Contract.Implementation
{
    internal class CommunicationContract : ICommunicationContract
    {
        private readonly ICommunicationDbContext _communicationDbContext;

        public CommunicationContract(ICommunicationDbContext communicationDbContext)
        {
            _communicationDbContext = communicationDbContext;
        }

        public Task<int> GetOrderEmailsCountAsync(int orderId, CancellationToken cancellationToken)
        {
            return _communicationDbContext.Emails.CountAsync(x => x.OrderId == orderId, cancellationToken);
        }
    }
}
