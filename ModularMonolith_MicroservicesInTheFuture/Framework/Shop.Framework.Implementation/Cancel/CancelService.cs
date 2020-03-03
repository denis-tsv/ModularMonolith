using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shop.Framework.Interfaces.Cancel;

namespace Shop.Framework.Implementation.Cancel
{
    internal class CancelService : ICancelService
    {
        private readonly IServiceProvider _serviceProvider;

        public CancelService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        private readonly List<(Type CancelHandlerType, ICancel Cancel)> _cancels = new List<(Type CancelHandlerType, ICancel Cancel)>();
        public void AddCancel<TCancel, TCancelHandler>(TCancel cancel) where TCancel : ICancel where TCancelHandler : ICancelHandler<TCancel>
        {
            _cancels.Add((typeof(TCancelHandler), cancel));
        }

        public async Task CancelAllAsync()
        {
            var tasks = new List<Task>();
            foreach (var (cancelHandlerType, cancel) in _cancels)
            {
                var cancelHandler = _serviceProvider.GetRequiredService(cancelHandlerType);
                Task task = (Task)cancelHandlerType.GetMethod("HandleAsync").Invoke(cancelHandler, new object[] { cancel });
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
        }
    }
}
