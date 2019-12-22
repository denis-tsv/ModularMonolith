using Microsoft.EntityFrameworkCore;
using Shop.Common.DataAccess.MsSql;
using Shop.Common.Entities;
using Shop.Order.DataAccess.MsSql;
using Shop.Order.Entities;
using System;
using System.Transactions;
using Microsoft.Data.SqlClient;
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
            var commonOptions = new DbContextOptionsBuilder<CommonDbContext>()
               .UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=ModularMonolith;Integrated Security=True")
               .Options;
            var commonDbContext = new CommonDbContext(commonOptions);

            var orderOptions = new DbContextOptionsBuilder<OrderDbContext>()
               .UseSqlServer("Data Source=.;Initial Catalog=ModularMonolith;Integrated Security=True")
               .Options;
            var orderDbContext = new OrderDbContext(orderOptions);

            using var scope = new TransactionScope();
            commonDbContext.Emails.Add(new Email { Subject = "Sbj", Body = "Body", Address = "test@test.test" });
            commonDbContext.SaveChanges();

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

            var commonOptions = new DbContextOptionsBuilder<CommonDbContext>()
                .UseSqlServer(connection)
                .Options;
            var commonDbContext = new CommonDbContext(commonOptions);

            var orderOptions = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(connection)
                .Options;
            var orderDbContext = new OrderDbContext(orderOptions);

            commonDbContext.Emails.Add(new Email { Subject = "Sbj", Body = "Body", Address = "test@test.test" });
            commonDbContext.SaveChanges();

            orderDbContext.Products.Add(new Product { Name = "Prod1", Price = 1 });
            orderDbContext.SaveChanges();

            scope.Complete();
        }        
    }
}
