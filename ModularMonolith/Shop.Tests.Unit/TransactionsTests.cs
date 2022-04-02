using Microsoft.EntityFrameworkCore;
using Shop.Order.DataAccess.MsSql;
using System;
using System.Linq;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Shop.Communication.DataAccess.MsSql;
using Shop.Communication.Entities;
using Xunit;

namespace Shop.Tests.Unit
{
    //https://docs.microsoft.com/en-us/ef/core/saving/transactions
    public class TransactionsTests
    {
        [Fact]
        public void DbTransaction_Rollback()
        {
            var connectionString = GetConnectionString();
            var connection = new SqlConnection(connectionString);
            connection.Open(); //required to begin transaction

            var transaction = connection.BeginTransaction();
            
            var communicationOptions = new DbContextOptionsBuilder<CommunicationDbContext>()
                .UseSqlServer(connection)
                .Options;
            var communicationDbContext = new CommunicationDbContext(communicationOptions);
            communicationDbContext.Database.UseTransaction(transaction);

            var orderOptions = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(connection)
                .Options;
            var orderDbContext = new OrderDbContext(orderOptions);
            orderDbContext.Database.UseTransaction(transaction);

            var order = new Order.Entities.Order();
            orderDbContext.Orders.Add(order);
            orderDbContext.SaveChanges();

            communicationDbContext.Emails.Add(new Email { Subject = "Sbj", Body = "Body", Address = "test@test.test", OrderId = order.Id });
            communicationDbContext.SaveChanges();

            transaction.Rollback();

            var orderDbContext1 = new OrderDbContext(orderOptions);
            var cnt = orderDbContext1.Orders.Count(x => x.Id == order.Id);

            Assert.Equal(0, cnt);
        }

        [Fact]
        public void DbTransaction_Commit()
        {
            var connectionString = GetConnectionString();
            var connection = new SqlConnection(connectionString);
            connection.Open(); //required to begin transaction

            var transaction = connection.BeginTransaction();

            var communicationOptions = new DbContextOptionsBuilder<CommunicationDbContext>()
                .UseSqlServer(connection)
                .Options;
            var communicationDbContext = new CommunicationDbContext(communicationOptions);
            communicationDbContext.Database.UseTransaction(transaction);

            var orderOptions = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(connection)
                .Options;
            var orderDbContext = new OrderDbContext(orderOptions);
            orderDbContext.Database.UseTransaction(transaction);

            var order = new Order.Entities.Order();
            orderDbContext.Orders.Add(order);
            orderDbContext.SaveChanges();

            communicationDbContext.Emails.Add(new Email { Subject = "Sbj", Body = "Body", Address = "test@test.test", OrderId = order.Id });
            communicationDbContext.SaveChanges();

            transaction.Commit();

            var newOrderContext = new OrderDbContext(orderOptions);
            var newCommContext = new CommunicationDbContext(communicationOptions);

            var ordersCount = newOrderContext.Orders.Count(x => x.Id == order.Id);
            var emailsCount = communicationDbContext.Emails.Count(x => x.OrderId == order.Id);

            Assert.Equal(1, ordersCount);
            Assert.Equal(1, emailsCount);
        }

        [Fact]
        public void TransactionScope_ConnectionString_Dispose()
        {
            var connectionString = GetConnectionString();

            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

            var communicationOptions = new DbContextOptionsBuilder<CommunicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var communicationDbContext = new CommunicationDbContext(communicationOptions);

            var orderOptions = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var orderDbContext = new OrderDbContext(orderOptions);

            var order = new Order.Entities.Order();
            orderDbContext.Orders.Add(order);
            orderDbContext.SaveChanges();

            communicationDbContext.Emails.Add(new Email { Subject = "Sbj", Body = "Body", Address = "test@test.test", OrderId = order.Id});
            communicationDbContext.SaveChanges();
            
            scope.Dispose();

            var orderDbContext1 = new OrderDbContext(orderOptions);
            var cnt = orderDbContext1.Orders.Count(x => x.Id == order.Id);
            
            Assert.Equal(0, cnt);
        }

        [Fact]
        public void TransactionScope_Connection_Dispose()
        {
            var connectionString = GetConnectionString();
            var connection = new SqlConnection(connectionString);

            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

            var communicationOptions = new DbContextOptionsBuilder<CommunicationDbContext>()
                .UseSqlServer(connection)
                .Options;
            var communicationDbContext = new CommunicationDbContext(communicationOptions);

            var orderOptions = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(connection)
                .Options;
            var orderDbContext = new OrderDbContext(orderOptions);

            var order = new Order.Entities.Order();
            orderDbContext.Orders.Add(order);
            orderDbContext.SaveChanges();

            communicationDbContext.Emails.Add(new Email { Subject = "Sbj", Body = "Body", Address = "test@test.test", OrderId = order.Id });
            communicationDbContext.SaveChanges();

            scope.Dispose();

            var orderDbContext1 = new OrderDbContext(orderOptions);
            var cnt = orderDbContext1.Orders.Count(x => x.Id == order.Id);

            Assert.Equal(0, cnt);
        }

        [Fact]
        public void CommittableTransaction_Connection_Rollback()
        {
            var connectionString = GetConnectionString();
            var connection = new SqlConnection(connectionString);
            connection.Open();

            using var transaction = new CommittableTransaction(
                       new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

            var communicationOptions = new DbContextOptionsBuilder<CommunicationDbContext>()
                .UseSqlServer(connection)
                .Options;
            var communicationDbContext = new CommunicationDbContext(communicationOptions);
            communicationDbContext.Database.EnlistTransaction(transaction);

            var orderOptions = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(connection)
                .Options;
            var orderDbContext = new OrderDbContext(orderOptions);
            orderDbContext.Database.EnlistTransaction(transaction);

            var order = new Order.Entities.Order();
            orderDbContext.Orders.Add(order);
            orderDbContext.SaveChanges();

            communicationDbContext.Emails.Add(new Email { Subject = "Sbj", Body = "Body", Address = "test@test.test", OrderId = order.Id });
            communicationDbContext.SaveChanges();

            transaction.Rollback();

            var newOpts = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var orderDbContext1 = new OrderDbContext(newOpts);
            var cnt = orderDbContext1.Orders.Count(x => x.Id == order.Id);

            Assert.Equal(0, cnt);
        }

        [Fact]
        public void CommittableTransaction_ConnectionString_PlatformNotSupportedException()
        {
            var connectionString = GetConnectionString();
            
            using var transaction = new CommittableTransaction(
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

            var communicationOptions = new DbContextOptionsBuilder<CommunicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var communicationDbContext = new CommunicationDbContext(communicationOptions);
            communicationDbContext.Database.OpenConnection();
            communicationDbContext.Database.EnlistTransaction(transaction);

            var orderOptions = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var orderDbContext = new OrderDbContext(orderOptions);
            orderDbContext.Database.OpenConnection();
            Assert.Throws<PlatformNotSupportedException>(() => orderDbContext.Database.EnlistTransaction(transaction));
        }

        private string GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration.GetConnectionString("MsSqlConnection");
        }
    }
}
