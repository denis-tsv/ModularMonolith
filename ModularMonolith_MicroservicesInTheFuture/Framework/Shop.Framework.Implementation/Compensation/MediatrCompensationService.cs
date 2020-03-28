using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Shop.Framework.Interfaces.Compensation;

namespace Shop.Framework.Implementation.Compensation
{
    internal class MediatrCompensationService : ICompensationService
    {
        private readonly IMediator _mediator;

        public MediatrCompensationService(IMediator mediator)
        {
            _mediator = mediator;
        }
        private readonly List<IRequest> _compensationRequests = new List<IRequest>();
        public void AddRequest<TCompensationRequest>(TCompensationRequest cancel) where TCompensationRequest : IRequest
        {
            _compensationRequests.Add(cancel);
        }

        public async Task SendAllAsync()
        {
            var tasks = new List<Task>();
            foreach (var compensationRequest in _compensationRequests)
            {
                Task task = _mediator.Send(compensationRequest);
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
            _compensationRequests.Clear();
        }
    }
}
