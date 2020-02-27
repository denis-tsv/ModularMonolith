using System;
using System.Collections.Concurrent;
using Shop.Framework.Interfaces.CancelUseCase;

namespace Shop.Framework.Implementation.CancelUseCase
{
    internal class CancelUseCaseService : ICancelUseCaseService
    {
        
        private readonly ConcurrentDictionary<string, (Type CancelerType, ICancelContext Context)> _operations = new ConcurrentDictionary<string, (Type CancelerType, ICancelContext Context)>();
        
        public void Add<TContext, TCanceler>(string correlationId, TContext context)
            where TContext : ICancelContext
            where TCanceler : ICancelUseCase<TContext>
        {
            if (!_operations.TryAdd(correlationId, (typeof(TCanceler), context)))
                throw new Exception("Unable to add operation");
        }

        public bool TryGet(string correlationId, out (Type CancelerType, ICancelContext Context) outData)
        {
            return _operations.TryRemove(correlationId, out outData);
        }
    }
}
