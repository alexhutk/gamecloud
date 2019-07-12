using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SteamKiller.BLL.Entities.Configurations;
using SteamKiller.BLL.Interfaces;
using SteamKiller.BLL.Services;
using SteamKiller.BLL.Services.Implementation;
using SteamKiller.BLL.Services.Interfaces;

namespace SteamKiller.BLL.Infrastructure.DI
{
    public static class ServiceProviderExtensions
    {
        public static void AddBusinessServices(this IServiceCollection services, string rootPath)
        {
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<ILeaderboardService, LeaderboardService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAchievmentService, AchievmentService>();
            services.AddTransient<ICloudSaveService, CloudSaveService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IResourceService, ResourceService>();
            services.AddTransient<IFinanceService, FinanceService>();
            services.AddSingleton<ISenderService, EmailSenderService>();
            services.AddSingleton<IReportService, ReportService>();
            services.AddSingleton<EmailConfiguration>();
            services.AddSingleton<ResourceConfiguration>(new ResourceConfiguration(rootPath));
            services.AddSingleton<ISecurityService, SecurityService>();
        }
    }
}
