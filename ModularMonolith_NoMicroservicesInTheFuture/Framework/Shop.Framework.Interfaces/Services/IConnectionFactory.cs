using System.Data.Common;

namespace Shop.Framework.Interfaces.Services
{
    public interface IConnectionFactory 
    {
        DbConnection GetConnection();
        DbTransaction GetTransaction();
        bool IsConnectionOpened { get; }
        bool IsTransactionStarted { get; }
    }
}
