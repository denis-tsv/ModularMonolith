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
using Shop.Order.Contract.Implementation;
using Shop.Order.DataAccess.MsSql;
using Shop.Order.UseCases;
using Shop.Order.UseCases.Orders.Dto;
using Shop.Utils.Modules;
using Shop.Web.Sagas;
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
            
            var saga = serviceProvider.GetRequiredService<CreateOrderSaga>();
            var dto = new CreateOrderDto { Items = new[] { new OrderItemDto { Count = 1, ProductId = 1 } } };
            
            //act
            saga.Start(dto);

            //assert
            var orderId = saga.GetResult();
            Assert.NotNull(orderId);

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
            //TODO Decorator doesn't work
            //services.RegisterModule<CommunicationDataAccessModule>(configuration);
            //services.Decorate<ICommunicationDbContext, TestCommunicationDbContext>();
            services.AddDbContext<CommunicationDbContext>((sp, bld) =>
            {
                bld.UseSqlServer(configuration.GetConnectionString("MsSqlConnection"));
            });
            services.AddScoped<ICommunicationDbContext, TestCommunicationDbContext>();

            var serviceProvider = services.BuildServiceProvider();

            var (orderDbContext, communicationDbContext) = await CreateDatabase(connectionString);

            var saga = serviceProvider.GetRequiredService<CreateOrderSaga>();
            var dto = new CreateOrderDto { Items = new[] { new OrderItemDto { Count = 1, ProductId = 1 } } };

            //act
            saga.Start(dto);

            //assert
            var orderId = saga.GetResult();
            Assert.Null(orderId);

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

            services.RegisterModule<OrderDataAccessModule>(configuration);

            services.RegisterModule<FrameworkModule>(configuration);
            services.RegisterModule<EmailModule>(configuration);

            services.RegisterModule<CommunicationInfrastructureModule>(configuration);
            services.RegisterModule<CommunicationContractModule>(configuration);
            services.RegisterModule<CommunicationUseCasesModule>(configuration);

            services.RegisterModule<OrderContractModule>(configuration);
            services.RegisterModule<OrderUseCasesModule>(configuration);

            services.AddScoped<CreateOrderSaga>();

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