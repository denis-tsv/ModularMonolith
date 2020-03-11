using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Framework.Interfaces.Messaging;
using Shop.Utils.Extensions;

namespace Shop.Framework.Implementation.Messaging.WaitingTasksStore
{
    internal class WaitingTasksStore : IWaitingTasksStore
    {
        private  readonly ConcurrentDictionary<(Guid CorrelationId, string TypeName), object> _waitingTasks = new ConcurrentDictionary<
            (Guid CorrelationId, string TypeName), object>();
        
        public Task<TMessage> Add<TMessage>(Guid correlationId) where TMessage : Message
        {
            if (correlationId == Guid.Empty) throw new InvalidOperationException("message.CorrelationId is null or empty");

            var tcs = _waitingTasks.GetOrAdd((correlationId, typeof(TMessage).Name), new TaskCompletionSource<TMessage>());
            return ((TaskCompletionSource<TMessage>)tcs).Task;
        }

        public bool TryComplete<TMessage>(TMessage message) where TMessage : Message
        {
            if (message is ExceptionMessage exceptionMessage)
            {
                return CompleteException(exceptionMessage);
            }
            else
            {
                return CompleteResult(message);
            }
        }

        private bool CompleteResult<TMessage>(TMessage message) where TMessage : Message
        {
            if (!_waitingTasks.TryRemove((message.CorrelationId, typeof(TMessage).Name), out var obj))
                return false;
            
            var tcs = (TaskCompletionSource<TMessage>) obj;
            
            tcs.SetResult(message);

            return true;
        }

        private bool CompleteException<TMessage>(TMessage message) where TMessage : ExceptionMessage
        {
            var keys = _waitingTasks.Keys
                .Where(x => x.CorrelationId == message.CorrelationId)
                .ToList();
            if (!keys.Any()) return false;

            foreach (var key in keys)
            {
                _waitingTasks.Remove(key, out var obj);

                dynamic tcs = obj;

                tcs.SetException(message.Exception);
            }

            return true;
        }
    }
}
