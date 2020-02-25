using System.Threading.Tasks;

namespace Shop.Utils.CancelUseCase
{
    public interface ICancelUseCase<TContext> where TContext : ICancelContext
    {
        Task CancelAsync(TContext context);
    }
}
