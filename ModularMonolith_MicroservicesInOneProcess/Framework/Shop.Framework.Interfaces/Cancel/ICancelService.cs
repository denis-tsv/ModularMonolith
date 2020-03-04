using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Cancel
{
    public interface ICancelService
    {
        void AddCancel<TCancel>(string correlationId, TCancel cancel)
            where TCancel : ICancel;

        void RemoveAll(string correlationId);
        Task CancelAllAsync(string correlationId);

    }
}
