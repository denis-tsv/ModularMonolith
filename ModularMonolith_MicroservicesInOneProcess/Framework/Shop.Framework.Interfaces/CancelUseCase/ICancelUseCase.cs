using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.CancelUseCase
{
    public interface ICancelUseCase<TContext> where TContext : ICancelContext
    {
        Task CancelAsync(TContext context);
    }
}
