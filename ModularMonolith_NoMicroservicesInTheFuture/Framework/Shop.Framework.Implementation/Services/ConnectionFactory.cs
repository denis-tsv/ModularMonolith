﻿using System.Data.Common;
using Microsoft.Data.SqlClient;
using Shop.Framework.Interfaces.Services;

namespace Shop.Framework.Implementation.Services
{
    internal class ConnectionFactory : IConnectionFactory
    {
        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        private DbConnection _connection;
        private DbTransaction _transaction;
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

        public DbTransaction GetTransaction()
        {
            return _transaction ?? (_transaction = GetConnection().BeginTransaction());
        }
    }
}