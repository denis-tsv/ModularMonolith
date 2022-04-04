using System.Threading;
using System.Threading.Tasks;

namespace Shop.Communication.Contract
{
    public interface ICommunicationContract
    {
        Task<int> GetOrderEmailsCountAsync(int orderId, CancellationToken cancellationToken);
    }
}
