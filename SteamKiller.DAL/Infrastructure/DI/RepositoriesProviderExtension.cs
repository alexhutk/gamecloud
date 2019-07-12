using Microsoft.Extensions.DependencyInjection;
using SteamKiller.DAL.EntitiesFramefork;
using SteamKiller.DAL.Implementation.Repositories;
using SteamKiller.DAL.Interfaces;
using SteamKiller.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Infrastructure.DI
{
    public static class RepositoriesProviderExtension
    {
        public static void AddRepositoriesServices(this IServiceCollection services)
        {
            services.AddTransient<IAchievmentRepository, AchievmentRepository>();
            services.AddTransient<IAppAchRepository, AppAchRepository>();
            services.AddTransient<IAccAchRepository, AccAchRepository>();
            services.AddTransient<IAchievmentRepository, AchievmentRepository>();
            services.AddTransient<ILeaderboardRepository, LeaderboardRepository>();
            services.AddTransient<IAccLeaderRepository, AccLeaderRepository>();
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAppAccRepository, AppAccRepository>();
            services.AddTransient<ICloudSaveRepository, CloudSaveRepository>();
            services.AddTransient<IAppAccSaveRepository, AppAccSaveRepository>();
            services.AddTransient<IAvatarRepository, AvatarRepository>();
            services.AddTransient<IFinanceProfileRepository, FinanceProfileRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
