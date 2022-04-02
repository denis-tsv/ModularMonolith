using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Communication.BackgroundJobs;
using Shop.Communication.Contract.Implementation;
using Shop.Communication.DataAccess.Interfaces;
using Shop.Communication.DataAccess.MsSql;
using Shop.Communication.Entities;
using Shop.Communication.UseCases;
using Shop.Emails.Implementation;
using Shop.Framework.UseCases.Implementation;
using Shop.Framework.UseCases.Interfaces.Services;
using Shop.Order.Contract.Implementation;
using Shop.Order.DataAccess.MsSql;
using Shop.Order.UseCases;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Order.UseCases.Orders.Dto;
using Shop.Utils.Modules;
using Shop.Web.Utils;
using Xunit;

namespace Shop.Tests.Unit
{
    public class WorkflowTests
    {
        [Fact]
        public async Task Should_Create_Order_And_Email()
        {
            //arrange
            var (connectionString, configuration) = CreateConfiguration();

            var services = CreateServiceProvider(configuration);
            services.RegisterModule<CommunicationDataAccessModule>(configuration);
            var serviceProvider = services.BuildServiceProvider();

            var (orderDbContext, communicationDbContext) = await CreateDatabase(connectionString);
            
            var sender = serviceProvider.GetRequiredService<ISender>();
            var dto = new CreateOrderDto { Items = new[] { new OrderItemDto { Count = 1, ProductId = 1 } } };
            
            //act
            var orderId = await sender.Send(new CreateOrderRequest { CreateOrderDto = dto });

            //assert
            var order = await orderDbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            var email = await communicationDbContext.Emails.FirstOrDefaultAsync(x => x.OrderId == orderId);
            
            Assert.NotNull(order);
            Assert.NotNull(email);

            await orderDbContext.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_Not_Create_Order_And_Email_On_Error()
        {
            //arrange
            var (connectionString, configuration) = CreateConfiguration();

            var services = CreateServiceProvider(configuration);
            services.AddDbContext<CommunicationDbContext>((sp, bld) =>
            {
                var factory = sp.GetRequiredService<IConnectionFactory>();
                bld.UseSqlServer(factory.GetConnection());
            });
            services.AddScoped<ICommunicationDbContext>(sp =>
            {
                var context = sp.GetRequiredService<CommunicationDbContext>();
                var connectionFactory = sp.GetRequiredService<IConnectionFactory>();

                context.Database.UseTransaction(connectionFactory.GetTransaction());

                var testContext = new TestCommunicationDbContext(context);

                return testContext;
            });
            var serviceProvider = services.BuildServiceProvider();

            var (orderDbContext, communicationDbContext) = await CreateDatabase(connectionString);

            var sender = serviceProvider.GetRequiredService<ISender>();
            var dto = new CreateOrderDto { Items = new[] { new OrderItemDto { Count = 1, ProductId = 1 } } };

            //act
            await Assert.ThrowsAsync<Exception>(() => sender.Send(new CreateOrderRequest { CreateOrderDto = dto }));

            //assert
            var ordersCount = await orderDbContext.Orders.CountAsync();
            var emailsCount = await communicationDbContext.Emails.CountAsync();

            Assert.Equal(0, ordersCount);
            Assert.Equal(0, emailsCount);

            await orderDbContext.Database.EnsureDeletedAsync();
        }

        class TestCommunicationDbContext : ICommunicationDbContext
        {
            private readonly ICommunicationDbContext _context;

            public TestCommunicationDbContext(CommunicationDbContext context)
            {
                _context = context;
            }

            public DbSet<Email> Emails
            {
                get => _context.Emails;
                set => _context.Emails = value;
            }

            public int SaveChanges()
            {
                throw new Exception("Fail");
            }

            public Task<int> SaveChangesAsync(CancellationToken token = default)
            {
                throw new Exception("Fail");
            }
        }

        private (string, IConfigurationRoot) CreateConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = $"Data Source=.;Initial Catalog=Test_{Guid.NewGuid()};Integrated Security=True";
            configuration.GetSection("ConnectionStrings")["MsSqlConnection"] = connectionString;
            
            return (connectionString, configuration);
        }

        private ServiceCollection CreateServiceProvider(IConfiguration configuration)
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var assemblies = Directory.EnumerateFiles(Path.GetDirectoryName(location), "Shop*UseCases.dll")
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .ToArray();

            var services = new ServiceCollection();

            services.AddHttpContextAccessor();

            services.AddOptions();

            services.AddMediatR(assemblies);
            services.AddAutoMapper(assemblies);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DbTransactionPipelineBehavior<,>));

            services.RegisterModule<OrderDataAccessModule>(configuration);

            services.RegisterModule<FrameworkModule>(configuration);
            services.RegisterModule<EmailModule>(configuration);

            services.RegisterModule<CommunicationInfrastructureModule>(configuration);
            services.RegisterModule<CommunicationContractModule>(configuration);
            services.RegisterModule<CommunicationUseCasesModule>(configuration);

            services.RegisterModule<OrderContractModule>(configuration);
            services.RegisterModule<OrderUseCasesModule>(configuration);

            return services;
        }

        private async Task<(OrderDbContext, CommunicationDbContext)> CreateDatabase(string connectionString)
        {
            var orderOptions = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var orderDbContext = new OrderDbContext(orderOptions);
            await orderDbContext.Database.MigrateAsync();

            var commOptions = new DbContextOptionsBuilder<CommunicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var communicationDbContext = new CommunicationDbContext(commOptions);
            await communicationDbContext.Database.MigrateAsync();
            
            return (orderDbContext, communicationDbContext);
        }
    }
}