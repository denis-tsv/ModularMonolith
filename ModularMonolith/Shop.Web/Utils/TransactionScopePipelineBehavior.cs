using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;
using Shop.Framework.UseCases.Interfaces.Transactions;

namespace Shop.Web.Utils
{
    public class TransactionScopePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ITransactionalRequest
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);

            var result = await next();

            scope.Complete();

            return result;
        }
    }
}