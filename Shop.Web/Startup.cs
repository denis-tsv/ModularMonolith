using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Common.DataAccess.MsSql;
using Shop.Common.Infrastructure.Implementation;
using Shop.Identity.Controllers;
using Shop.Identity.DataAccess.MsSql;
using Shop.Identity.UseCases;
using Shop.Order.Controllers;
using Shop.Order.DataAccess.MsSql;
using Shop.Order.DomainServices.Implementation;
using Shop.Order.UseCases;
using Shop.Order.UseCases.Orders.Mappings;
using Shop.Utils.Modules;
using Shop.Web.Utils;

namespace Shop.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(OrdersAutoMapperProfile));
            
            services.AddOptions();

            services.AddMvc()
                .AddApplicationPart(typeof(OrdersController).Assembly)
                .AddApplicationPart(typeof(IdentityController).Assembly)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //TODO migrate to autofac
            services.RegisterModule<CommonDataAccessModule>(Configuration);
            services.RegisterModule<CommonInfrastructureModule>(Configuration);
            services.RegisterModule<IdentityDataAccessModule>(Configuration);
            services.RegisterModule<IdentityUseCasesModule>(Configuration);
            services.RegisterModule<OrderDataAccessModule>(Configuration);
            services.RegisterModule<OrderDomainServicesModule>(Configuration);
            services.RegisterModule<OrderUseCasesModule>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandlerMiddleware();

            app.UseMvc();
        }
    }
}
