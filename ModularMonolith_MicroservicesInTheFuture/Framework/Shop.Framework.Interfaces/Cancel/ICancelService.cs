using System.Threading.Tasks;
using MediatR;

namespace Shop.Framework.Interfaces.Cancel
{
    public interface ICancelService
    {
        void AddCancel<TCancelRequest>(TCancelRequest cancel)
            where TCancelRequest : IRequest;
            

        Task CancelAllAsync();
    }
}
