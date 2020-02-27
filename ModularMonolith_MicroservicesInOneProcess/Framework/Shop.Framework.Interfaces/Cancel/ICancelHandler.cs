using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Cancel
{
    public interface ICancelHandler<TCancel> where TCancel : ICancel
    {
        Task HandleAsync(TCancel cancel);
    }
}
