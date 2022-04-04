using System;
using System.Data.Common;

namespace Shop.Framework.UseCases.Interfaces.Services
{
    public interface IConnectionFactory : IDisposable
    {
        DbConnection GetConnection();
        bool IsConnectionOpened { get; }
    }
}
