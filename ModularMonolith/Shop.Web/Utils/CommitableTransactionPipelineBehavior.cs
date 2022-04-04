using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;
using Shop.Framework.UseCases.Interfaces.Services;
using Shop.Framework.UseCases.Interfaces.Transactions;

namespace Shop.Web.Utils
{
    public class CommitableTransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ITransactionalRequest
    {
        private readonly IConnectionFactory _connectionFactory;
        
        public CommitableTransactionPipelineBehavior(IConnectionFactory connectionFactory)
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

            using var transaction = new CommittableTransaction(
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

            _connectionFactory.SetTransaction(transaction);

            var result = await next();

            transaction.Commit();

            return result;
        }
    }
}