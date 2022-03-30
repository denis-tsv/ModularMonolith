using System.Threading;
using System.Threading.Tasks;

namespace Shop.Communication.Contract
{
    public interface ICommunicationContract
    {
        Task ScheduleOrderCreatedEmailAsync(string email, int orderId, string orderDetailsUrl, CancellationToken cancellationToken);
    }
}
