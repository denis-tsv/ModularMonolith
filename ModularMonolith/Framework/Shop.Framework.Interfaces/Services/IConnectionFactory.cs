using System;
using System.Data.Common;
using System.Transactions;

namespace Shop.Framework.UseCases.Interfaces.Services
{
    public interface IConnectionFactory : IDisposable
    {
        DbConnection GetConnection();
        Transaction GetTransaction();
        void SetTransaction(Transaction transaction);
        bool IsConnectionOpened { get; }
        bool IsTransactionStarted { get; }
    }
}
