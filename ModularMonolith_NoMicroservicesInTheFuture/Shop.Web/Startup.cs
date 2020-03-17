using Autofac;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Order.Controllers;
using Shop.Order.DataAccess.MsSql;
using Shop.Order.DomainServices.Implementation;
using Shop.Order.UseCases;
using Shop.Order.UseCases.Orders.Mappings;
using Shop.Utils.Modules;
using Shop.Web.Utils;
using Microsoft.Extensions.Hosting;
using Shop.Communication.Contract.Implementation;
using Shop.Communication.DataAccess.MsSql;
using Shop.Communication.Infrastructure.Implementation;
using Shop.Framework.Implementation;
using Shop.Order.Contract.Implementation;
using Shop.Communication.UseCases;

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

            services.AddControllers()
                .AddApplicationPart(typeof(OrdersController).Assembly);
            services.RegisterModule<FrameworkModule>(Configuration);

#if !DB_TRANSACTION
            services.RegisterModule<CommunicationDataAccessModule>(Configuration);
            services.RegisterModule<OrderDataAccessModule>(Configuration);
#endif
            services.RegisterModule<CommunicationInfrastructureModule>(Configuration);
            services.RegisterModule<CommunicationContractModule>(Configuration);
            services.RegisterModule<CommunicationUseCasesModule>(Configuration);

            services.RegisterModule<OrderContractModule>(Configuration);
            services.RegisterModule<OrderDomainServicesModule>(Configuration);
            services.RegisterModule<OrderUseCasesModule>(Configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
#if DB_TRANSACTION
            builder.RegisterGeneric(typeof(DbTransactionPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            
            builder.RegisterModule<CommunicationDataAccessAutofacModule>();
            builder.RegisterModule<OrderDataAccessAutofacModule>();
#else
            builder.RegisterGeneric(typeof(TransactionScopePipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
#endif
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
