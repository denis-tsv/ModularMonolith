using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Framework.Interfaces.Cancel;

namespace Shop.Framework.Implementation.Cancel
{
    internal class CancelService : ICancelService
    {
        private readonly ConcurrentDictionary<string, ICollection<(Type CancelHandlerType, ICancel Cancel)>> _operations = new ConcurrentDictionary<string, ICollection<(Type CancelHandlerType, ICancel Cancel)>>();
        private readonly IServiceProvider _serviceProvider;

        public CancelService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public void AddCancel<TCancel, TCancelHandler>(string correlationId, TCancel cancel)
            where TCancel : ICancel
            where TCancelHandler : ICancelHandler<TCancel>
        {
            var value = (typeof(TCancelHandler), cancel);
            var values = new List<(Type CancelHandlerType, ICancel Cancel)>
            {
                value
            };
            _operations.AddOrUpdate(correlationId, values, (key, prev) =>
            {
                prev.Add(value);
                return prev;
            });
        }

        public void RemoveAll(string correlationId)
        {
            _operations.Remove(correlationId, out var _);
        }

        public async Task CancelAllAsync(string correlationId)
        {
            _operations.Remove(correlationId, out var cancels);

            var tasks = new List<Task>();
            foreach (var cancel in cancels)
            {
                var cancelHandler = _serviceProvider.GetService(cancel.CancelHandlerType);
                Task task = (Task)cancel.CancelHandlerType.GetMethod("HandleAsync").Invoke(cancelHandler, new object[] { cancel.Cancel });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }
    }
}
