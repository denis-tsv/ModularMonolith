using Microsoft.EntityFrameworkCore;
using Shop.Order.DataAccess.MsSql;
using Shop.Order.Entities;
using System;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Shop.Communication.DataAccess.MsSql;
using Shop.Communication.Entities;
using Xunit;

namespace Shop.Tests.Unit
{
    //https://docs.microsoft.com/en-us/ef/core/saving/transactions
    public class TransactionsTests
    {
        [Fact]
        //https://stackoverflow.com/questions/56328832/transactionscope-throwing-exception-this-platform-does-not-support-distributed-t
        public void TransactionScope_DifferentConnections_ThrowsPlatformNotSupportedException()
        {
            var communicationOptions = new DbContextOptionsBuilder<CommunicationDbContext>()
               .UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=ModularMonolith;Integrated Security=True")
               .Options;
            var communicationDbContext = new CommunicationDbContext(communicationOptions);

            var orderOptions = new DbContextOptionsBuilder<OrderDbContext>()
               .UseSqlServer("Data Source=.;Initial Catalog=ModularMonolith;Integrated Security=True")
               .Options;
            var orderDbContext = new OrderDbContext(orderOptions);

            using var scope = new TransactionScope();
            communicationDbContext.Emails.Add(new Email { Subject = "Sbj", Body = "Body", Address = "test@test.test" });
            communicationDbContext.SaveChanges();

            orderDbContext.Products.Add(new Product { Name = "Prod1", Price = 1 });
            Assert.Throws<PlatformNotSupportedException>(() => orderDbContext.SaveChanges());

            //scope.Complete();
        }

        [Fact]
        public void TransactionScope_SingleConnection_Ok()
        {
            var connectionString = @"Data Source =.; Initial Catalog = ModularMonolith; Integrated Security = True";

            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });
            
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var communicationOptions = new DbContextOptionsBuilder<CommunicationDbContext>()
                .UseSqlServer(connection)
                .Options;
            var communicationDbContext = new CommunicationDbContext(communicationOptions);

            var orderOptions = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(connection)
                .Options;
            var orderDbContext = new OrderDbContext(orderOptions);

            communicationDbContext.Emails.Add(new Email { Subject = "Sbj", Body = "Body", Address = "test@test.test" });
            communicationDbContext.SaveChanges();

            orderDbContext.Products.Add(new Product { Name = "Prod1", Price = 1 });
            orderDbContext.SaveChanges();

            scope.Complete();
        }        
    }
}
