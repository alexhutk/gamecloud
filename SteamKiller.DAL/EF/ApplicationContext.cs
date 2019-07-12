using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SteamKiller.DAL.Entites;
using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Entities;
using SteamKiller.DAL.Entities.Links;
using SteamKiller.DAL.Interfaces;

namespace SteamKiller.DAL.EntitiesFramefork
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Leaderboard> Leaderboards { get; set; }
        public DbSet<Achievment> Achievments { get; set; }
        public DbSet<CloudSave> CloudSaves { get; set; }
        public DbSet<AccLeader> AccLeaders { get; set; }
        public DbSet<AppAcc> AppAccs { get; set; }
        public DbSet<AccAch> AccAches { get; set; }
        public DbSet<AppAch> AppAches { get; set; }
        public DbSet<AppAccSave> AppAccSaves { get; set; }
        public DbSet<FinanceProfile> FinanceProfiles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
