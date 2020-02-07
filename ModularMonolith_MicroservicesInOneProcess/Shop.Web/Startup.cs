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
using Shop.Order.Contract.Implementation;
using Shop.Utils.ServiceBus;
using Shop.Web.Handlers;
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

            services.AddMediatR(typeof(FinishedOrderCreationMessageHandler));

            services.AddOptions();

            services.AddControllers();

            services.AddScoped<IServiceBus, ServiceBus>();
            services.AddSingleton<IWaitingTasksStore, WaitingTasksStore>();

            services.RegisterModule<CommonDataAccessModule>(Configuration);
            services.RegisterModule<CommonInfrastructureModule>(Configuration);
            services.RegisterModule<CommonContractModule>(Configuration);

            services.RegisterModule<IdentityDataAccessModule>(Configuration);
            services.RegisterModule<IdentityUseCasesModule>(Configuration);
            services.RegisterModule<IdentityContractModule>(Configuration);

            services.RegisterModule<OrderDataAccessModule>(Configuration);
            services.RegisterModule<OrderDomainServicesModule>(Configuration);
            services.RegisterModule<OrderUseCasesModule>(Configuration);
            services.RegisterModule<OrderContractModule>(Configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            
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
