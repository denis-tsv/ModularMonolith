using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;
using Shop.Framework.UseCases.Interfaces.Services;
using Shop.Framework.UseCases.Interfaces.Transactions;

namespace Shop.Web.Utils
{
    public class TransactionScopePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ITransactionalRequest
    {
        private readonly IConnectionFactory _connectionFactory;
        
        public TransactionScopePipelineBehavior(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_connectionFactory.IsConnectionOpened)
            {
                return await next();
            }

            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);

            await using var connection = _connectionFactory.GetConnection();
            
            var result = await next();

            scope.Complete();

            return result;
        }
    }
}