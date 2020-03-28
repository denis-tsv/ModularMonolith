using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Shop.Framework.Interfaces.Compensation;

namespace Shop.Framework.Implementation.Compensation
{
    public class SendCompensationsRequestExceptionAction<TRequest> : IRequestExceptionAction<TRequest>
    {
        private readonly ICompensationService _compensationService;

        public SendCompensationsRequestExceptionAction(ICompensationService compensationService)
        {
            _compensationService = compensationService;
        }
        public async Task Execute(TRequest request, Exception exception, CancellationToken cancellationToken)
        {
            await _compensationService.SendAllAsync();
        }
    }
}
