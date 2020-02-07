using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Shop.Utils.WaitingTasksStore
{
    public class WaitingTasksStore : IWaitingTasksStore
    {
        private  readonly ConcurrentDictionary<string, object> _waitingTasks = new ConcurrentDictionary<string, object>();
        public Task Add(string correlationId)
        {
            return Add<object>(correlationId);
        }

        public Task<T> Add<T>(string correlationId)
        {
            var tcs = new TaskCompletionSource<T>();

            if (!_waitingTasks.TryAdd(correlationId, tcs))
                throw new Exception($"Waiting task with correlation id '{correlationId}' already exists");

            return tcs.Task;
        }

        public void Complete<T>(string correlationId, T value)
        {
            if (!_waitingTasks.TryRemove(correlationId, out var obj))
                throw new Exception($"Waiting task with correlation id '{correlationId}' does not exist");
            
            var tcs = (TaskCompletionSource<T>) obj;
            
            tcs.SetResult(value);
        }

        public void Complete(string correlationId)
        {
            Complete<object>(correlationId, null);
        }

        public void CompleteException(string correlationId, Exception exception)
        {
            if(!_waitingTasks.TryRemove(correlationId, out var obj))
                throw new Exception($"Waiting task with correlation id '{correlationId}' does not exist");

            dynamic tcs = obj;

            tcs.SetException(exception);
        }
    }
}
