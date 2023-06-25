using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SteamKiller.BLL.Services.Implementation;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.DAL.EntitiesFramefork;
using SteamKiller.DAL.Migrations;
using System;

namespace SteamKiller.DPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationContext>();
                    var security = services.GetRequiredService<ISecurityService>();
                    var resource = services.GetRequiredService<IResourceService>();
                    DbSeeder.DbSeed(context, security, resource);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
