using System.Threading.Tasks;
using MediatR;

namespace Shop.Framework.Interfaces.Compensation
{
    public interface ICompensationService
    {
        void AddRequest<TCompensationRequest>(TCompensationRequest compensationRequest)
            where TCompensationRequest : IRequest;
            

        Task SendAllAsync();
    }
}
