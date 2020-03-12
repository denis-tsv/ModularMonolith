using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Shop.Framework.Interfaces.Cancel;

namespace Shop.Framework.Implementation.Cancel
{
    internal class MediatrCancelService : ICancelService
    {
        private readonly IMediator _mediator;

        public MediatrCancelService(IMediator mediator)
        {
            _mediator = mediator;
        }
        private readonly List<IRequest> _cancels = new List<IRequest>();
        public void AddCancel<TCancel>(TCancel cancel) where TCancel : IRequest
        {
            _cancels.Add(cancel);
        }

        public async Task CancelAllAsync()
        {
            var tasks = new List<Task>();
            foreach (var cancel in _cancels)
            {
                Task task = _mediator.Send(cancel);
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
            _cancels.Clear();
        }
    }
}
