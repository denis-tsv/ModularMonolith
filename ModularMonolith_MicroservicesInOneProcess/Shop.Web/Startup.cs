using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Order.DataAccess.MsSql;
using Shop.Order.UseCases;
using Shop.Order.UseCases.Orders.Mappings;
using Shop.Utils.Modules;
using Shop.Web.Utils;
using Microsoft.Extensions.Hosting;
using Shop.Communication.DataAccess.MsSql;
using Shop.Communication.Infrastructure.Implementation;
using Shop.Communication.UseCases;
using Shop.Framework.Implementation;

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
            
            services.AddControllers();
            
            services.RegisterModule<FrameworkModule>(Configuration);

            services.RegisterModule<CommunicationDataAccessModule>(Configuration);
            services.RegisterModule<CommunicationInfrastructureModule>(Configuration);
            services.RegisterModule<CommunicationUseCasesModule>(Configuration);

            services.RegisterModule<OrderDataAccessModule>(Configuration);
            services.RegisterModule<OrderUseCasesModule>(Configuration);
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
