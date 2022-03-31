using System.Threading;
using System.Threading.Tasks;

namespace Shop.Communication.Contract
{
    public interface ICommunicationContract
    {
        Task ScheduleOrderCreatedEmailAsync(string email, int orderId, CancellationToken cancellationToken);
    }
}
