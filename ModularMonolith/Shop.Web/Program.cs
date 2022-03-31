using System.Linq;
using FluentScheduler;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shop.Web.BackgroundJobsConfig;
using Microsoft.Extensions.Hosting;

namespace Shop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            JobManager.JobFactory = new JobFactory(webHost.Services);
            var regs = webHost.Services.GetServices<Registry>();
            JobManager.Initialize(regs.ToArray());
            JobManager.JobException += info =>
            {
                var logger = webHost.Services.GetRequiredService<ILogger<Program>>();
                logger.LogError(info.Exception, "Unhandled exception in job");
            };

            webHost.Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
