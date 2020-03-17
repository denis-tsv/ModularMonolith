using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Framework.Interfaces.Services;
using Shop.Framework.Interfaces.Transactions;

namespace Shop.Web.Utils
{
    public class DbTransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ITransactionalRequest
    {
        private readonly IConnectionFactory _connectionFactory;
        public DbTransactionPipelineBehavior(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_connectionFactory.IsTransactionStarted)
            {
                return await next();
            }

            await using var connection = _connectionFactory.GetConnection();
            await using var transaction = _connectionFactory.GetTransaction();

            var result = await next();

            transaction.Commit();

            return result;
        }
    }
}
