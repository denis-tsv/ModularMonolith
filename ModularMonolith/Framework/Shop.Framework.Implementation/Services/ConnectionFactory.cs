﻿using System.Data.Common;
using Microsoft.Data.SqlClient;
using Shop.Framework.UseCases.Interfaces.Services;

namespace Shop.Framework.UseCases.Implementation.Services
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

        public bool IsConnectionOpened => _connection != null;

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
