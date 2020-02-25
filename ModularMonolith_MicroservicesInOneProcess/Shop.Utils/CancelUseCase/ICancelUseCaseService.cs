using System;

namespace Shop.Utils.CancelUseCase
{
    public interface ICancelUseCaseService
    {
        void Add<TContext, TCanceler>(string correlationId, TContext context)
            where TContext : ICancelContext
            where TCanceler : ICancelUseCase<TContext>;

        bool TryGet(string correlationId, out (Type CancelerType, ICancelContext Context) res);
    }
}
