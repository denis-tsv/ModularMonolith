using System.Linq;
using Autofac;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Order.DataAccess.MsSql;
using Shop.Order.UseCases;
using Shop.Utils.Modules;
using Shop.Web.Utils;
using Microsoft.Extensions.Hosting;
using Shop.Communication.BackgroundJobs;
using Shop.Communication.Contract.Implementation;
using Shop.Communication.DataAccess.MsSql;
using Shop.Order.Contract.Implementation;
using Shop.Communication.UseCases;
using Shop.Emails.Implementation;
using Shop.Framework.UseCases.Implementation;

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

            services.AddOptions();

            services.AddControllers()
                .ConfigureApplicationPartManager(manager => manager.FeatureProviders.Add(new InternalControllerFeatureProvider()));

            services.RegisterModule<FrameworkModule>(Configuration);
            services.RegisterModule<EmailModule>(Configuration);

#if !DB_TRANSACTION
            services.RegisterModule<CommunicationDataAccessModule>(Configuration);
            services.RegisterModule<OrderDataAccessModule>(Configuration);
#endif
            services.RegisterModule<CommunicationInfrastructureModule>(Configuration);
            services.RegisterModule<CommunicationContractModule>(Configuration);
            services.RegisterModule<CommunicationUseCasesModule>(Configuration);

            services.RegisterModule<OrderContractModule>(Configuration);
            services.RegisterModule<OrderUseCasesModule>(Configuration);

            var sp = services.BuildServiceProvider();

            var requests = sp.GetServices<IBaseRequest>();
            services.AddMediatR(requests.Select(x => x.GetType()).ToArray());

            var profiles = sp.GetServices<Profile>();
            //not works when profiles registered in other modules
            services.AddAutoMapper(profiles.Select(x => x.GetType()).ToArray());
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
