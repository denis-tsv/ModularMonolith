using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.DataAccess.MsSql;
using Shop.Infrastructure.Implementation.Services;
using Shop.Infrastructure.Interfaces.DataAccess;
using Shop.Infrastructure.Interfaces.Services;
using Shop.UseCases.Orders.Commands.CreateOrder;
using Shop.UseCases.Orders.Mappings;
using Shop.Web.Utils;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Implementation.BackgroundJobs;
using Shop.Infrastructure.Interfaces.Options;

namespace Shop.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpContextAccessor();

            services.AddTransient<SendEmailsJob>();
            services.Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            
            services.AddMediatR(typeof(CreateOrderRequestHandler));
            services.AddAutoMapper(typeof(OrdersAutoMapperProfile));

            services.AddDbContext<IDbContext, AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection")));
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
