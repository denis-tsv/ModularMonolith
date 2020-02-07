using System;
using System.Threading.Tasks;

namespace Shop.Utils.WaitingTasksStore
{
    public interface IWaitingTasksStore
    {
        Task Add(string correlationId);
        Task<T> Add<T>(string correlationId);
        void Complete(string correlationId);
        void Complete<T>(string correlationId, T value);
        void CompleteException(string correlationId, Exception exception);
    }
}
