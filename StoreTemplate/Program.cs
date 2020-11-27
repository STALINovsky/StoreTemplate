using System;
using System.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StoreTemplateCore.Identity;

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


            var dbContext = serviceProvider.GetRequiredService<StoreDbContext>();
            await DbContextInitializer.TryInitEntities(dbContext, loggerFactory.CreateLogger("DbInitialization"));

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();       
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await DbContextInitializer.InitIdentity(userManager, roleManager, configuration);

        }
    }
}
