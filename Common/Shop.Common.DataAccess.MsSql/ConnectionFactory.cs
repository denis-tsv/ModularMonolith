using Shop.Utils.Connections;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Shop.Common.DataAccess.MsSql
{
    internal class ConnectionFactory : IConnectionFactory
    {
        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        private DbConnection _connection;
        private readonly string _connectionString;

        public DbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }
            return _connection;
        }
    }
}
