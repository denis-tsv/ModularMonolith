using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Shop.Framework.Interfaces.Cancel;

namespace Shop.Web.Utils
{
    public class CancelRequestExceptionAction<TRequest> : IRequestExceptionAction<TRequest>
    {
        private readonly ICancelService _cancelService;

        public CancelRequestExceptionAction(ICancelService cancelService)
        {
            _cancelService = cancelService;
        }
        public async Task Execute(TRequest request, Exception exception, CancellationToken cancellationToken)
        {
            await _cancelService.CancelAllAsync();
        }
    }
}
