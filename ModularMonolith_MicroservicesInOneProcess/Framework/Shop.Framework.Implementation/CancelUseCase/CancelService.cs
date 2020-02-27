using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Shop.Framework.Interfaces.Cancel;

namespace Shop.Framework.Implementation.CancelUseCase
{
    internal class CancelService : ICancelService
    {
        
        private readonly ConcurrentDictionary<string, ICollection<(Type CancelHandlerType, ICancel Cancel)>> _operations = new ConcurrentDictionary<string, ICollection<(Type CancelHandlerType, ICancel Cancel)>>();
        
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

        public bool TryRemoveCancel(string correlationId, out ICollection<(Type CancelHandlerType, ICancel Cancel)> outData)
        {
            return _operations.TryRemove(correlationId, out outData);
        }
    }
}
