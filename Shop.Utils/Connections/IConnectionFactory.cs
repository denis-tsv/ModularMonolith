using System.Data.Common;

namespace Shop.Utils.Connections
{
    public interface IConnectionFactory 
    {
        DbConnection GetConnection();
    }
}
