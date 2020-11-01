using System;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StoreTemplate
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await InitDataBase(host);

            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task InitDataBase(IHost host)
        {
            using var scope = host.Services.CreateScope();

            var serviceProvider = scope.ServiceProvider;
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            try
            {
                var dbContext = serviceProvider.GetRequiredService<StoreDbContext>();
                await DbContextInitializer.TryInitContext(dbContext,loggerFactory.CreateLogger("DbInitialization"));
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger("Program");
                logger.LogError(e,"An init DB");
            }
        }
    }
}
