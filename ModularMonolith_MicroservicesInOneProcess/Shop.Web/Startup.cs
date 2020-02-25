using Autofac;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Common.DataAccess.MsSql;
using Shop.Common.Infrastructure.Implementation;
using Shop.Identity.DataAccess.MsSql;
using Shop.Identity.UseCases;
using Shop.Order.DataAccess.MsSql;
using Shop.Order.DomainServices.Implementation;
using Shop.Order.UseCases;
using Shop.Order.UseCases.Orders.Mappings;
using Shop.Utils.Modules;
using Shop.Web.Utils;
using Microsoft.Extensions.Hosting;
using Shop.Common.Contract.Implementation;
using Shop.Identity.Contract.Implementation;
using Shop.Utils.CancelUseCase;
using Shop.Utils.Implementation.Messaging;
using Shop.Utils.Implementation.Services;
using Shop.Utils.Messaging;
using Shop.Utils.Services;
using Shop.Web.Utils.Dispatcher;
using Shop.Web.Utils.WaitingTasksStore;

namespace Shop.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(OrdersAutoMapperProfile));

            services.AddOptions();

            services.AddControllers();

            services.AddScoped<IMessageBroker, MediatrMessageBroker>(); //Singleton?
            services.AddSingleton<IWaitingTasksStore, WaitingTasksStore>();
            services.AddSingleton<IMessageDispatcher, MessageDispatcher>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<ICancelUseCaseService, CancelUseCaseService>();

            services.RegisterModule<CommonDataAccessModule>(Configuration);
            services.RegisterModule<CommonInfrastructureModule>(Configuration);
            services.RegisterModule<CommonContractModule>(Configuration);

            services.RegisterModule<IdentityDataAccessModule>(Configuration);
            services.RegisterModule<IdentityUseCasesModule>(Configuration);
            services.RegisterModule<IdentityContractModule>(Configuration);

            services.RegisterModule<OrderDataAccessModule>(Configuration);
            services.RegisterModule<OrderDomainServicesModule>(Configuration);
            services.RegisterModule<OrderUseCasesModule>(Configuration);
            
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(CompleteTaskMessageHandler<>)).As(typeof(INotificationHandler<>)).SingleInstance();
            builder.RegisterGeneric(typeof(CancelUseCaseMessageHandler<>)).As(typeof(INotificationHandler<>)).SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandlerMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
