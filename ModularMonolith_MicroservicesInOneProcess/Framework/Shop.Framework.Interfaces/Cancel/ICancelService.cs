using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Cancel
{
    public interface ICancelService
    {
        void AddCancel<TCancel, TCancelHandler>(string correlationId, TCancel cancel)
            where TCancel : ICancel
            where TCancelHandler : ICancelHandler<TCancel>;

        void RemoveAll(string correlationId);
        Task CancelAllAsync(string correlationId);

    }
}
