using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Shop.Web
{
    //TODO TRequest - transactional, generic constraint, need AutoFac
    public class TransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, 
                TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await next();

                scope.Complete();
                
                //TODO ConnectionFactory.GetConnection().Close(); optional improvement 

                return result;                                    
            }            
        }
    }
}
