using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Framework.Interfaces.Messaging;

namespace Shop.Framework.Implementation.Messaging.WaitingTasksStore
{
    internal class WaitingTasksStore : IWaitingTasksStore
    {
        //ConcurrentDictionary can be replaced by Dictionary because request processed in single thread
        private readonly ConcurrentDictionary<string, object> _waitingTasks = new ConcurrentDictionary<string, object>();
        
        public Task<TMessage> Add<TMessage>() where TMessage : Message
        {
            var tcs = _waitingTasks.GetOrAdd(typeof(TMessage).Name, new TaskCompletionSource<TMessage>());
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
            if (!_waitingTasks.TryRemove(typeof(TMessage).Name, out var obj))
                return false;
            
            var tcs = (TaskCompletionSource<TMessage>) obj;
            
            tcs.SetResult(message);

            return true;
        }

        private bool CompleteException<TMessage>(TMessage message) where TMessage : ExceptionMessage
        {
            var keys = _waitingTasks.Keys
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
