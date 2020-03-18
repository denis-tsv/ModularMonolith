using System;
using System.Data.Common;

namespace Shop.Framework.Interfaces.Services
{
    public interface IConnectionFactory : IDisposable
    {
        DbConnection GetConnection();
        DbTransaction GetTransaction();
        bool IsConnectionOpened { get; }
        bool IsTransactionStarted { get; }
    }
}
