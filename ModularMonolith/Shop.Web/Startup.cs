using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Order.DataAccess.MsSql;
using Shop.Order.UseCases;
using Shop.Web.Utils;
using Microsoft.Extensions.Hosting;
using Shop.Communication.BackgroundJobs;
using Shop.Communication.Contract.Implementation;
using Shop.Communication.DataAccess.MsSql;
using Shop.Order.Contract.Implementation;
using Shop.Communication.UseCases;
using Shop.Emails.Implementation;
using Shop.Framework.UseCases.Implementation;
using Shop.Utils.Modules;
using Shop.Web.StateMachines;

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

            services.AddOptions();

            services.AddControllers()
                .ConfigureApplicationPartManager(manager => manager.FeatureProviders.Add(new InternalControllerFeatureProvider()));

            services.RegisterModule<CommunicationDataAccessModule>(Configuration);
            services.RegisterModule<OrderDataAccessModule>(Configuration);

            services.RegisterModule<FrameworkModule>(Configuration);
            services.RegisterModule<EmailModule>(Configuration);

            services.RegisterModule<CommunicationInfrastructureModule>(Configuration);
            services.RegisterModule<CommunicationContractModule>(Configuration);
            services.RegisterModule<CommunicationUseCasesModule>(Configuration);

            services.RegisterModule<OrderContractModule>(Configuration);
            services.RegisterModule<OrderUseCasesModule>(Configuration);

            services.AddScoped<CreateOrderStateMachine>();

            var sp = services.BuildServiceProvider();

            var requests = sp.GetServices<IBaseRequest>();
            //MediatR works when AddMediatR calls in each module
            services.AddMediatR(requests.Select(x => x.GetType()).ToArray());

            var profiles = sp.GetServices<Profile>();
            //AutoMapper not works when AddAutoMapper calls in each module
            services.AddAutoMapper(profiles.Select(x => x.GetType()).ToArray());
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
