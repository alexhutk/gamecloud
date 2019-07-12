using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.DAL.EntitiesFramefork;
using SteamKiller.DAL.Entites;
using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Entities;
using SteamKiller.DAL.Entities.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SteamKiller.BLL.Services.Implementation;

namespace SteamKiller.DAL.Migrations
{
    public static class DbSeeder
    {
        public static void DbSeed(ApplicationContext context, ISecurityService security, IResourceService resources)
        {
            context.Database.EnsureCreated();

            if (context.Applications.Any())
                return;

            Random rnd = new Random();

            context.Applications.Add(
                new Application
                {
                    Name = "ASBU"
                }
            );

            context.Accounts.AddRange(new List<Account>()
            {
                new Account{ Name = "sonic", Password = security.GeneratePasswordHash("12345"), Avatar = resources.GetDefaultAvatar()},
                new Account{ Name = "pan_banan", Password = security.GeneratePasswordHash("12345"), Avatar = resources.GetDefaultAvatar()},
                new Account { Name = "lol", Password = security.GeneratePasswordHash("3333"), Avatar = resources.GetDefaultAvatar()}
            });

            context.Leaderboards.Add(new Leaderboard { Name = "ASBU_Leaderboard", ApplicationId = 1 });

            context.Achievments.AddRange(new List<Achievment>
            {
                new Achievment{ Name = "ACH_1", Description = "This is ach!", ImageData = Encoding.ASCII.GetBytes("20")},
                new Achievment{ Name = "ACH_2", Description = "This is ach!", ImageData = Encoding.ASCII.GetBytes("40")},
                new Achievment{ Name = "ACH_3", Description = "This is ach!", ImageData = Encoding.ASCII.GetBytes("50")}
            });

            context.CloudSaves.AddRange(new List<CloudSave>
            {
                new CloudSave{SaveData = Encoding.ASCII.GetBytes(rnd.Next(100).ToString()), SaveTime = DateTime.Now},
                new CloudSave{SaveData = Encoding.ASCII.GetBytes(rnd.Next(100).ToString()), SaveTime = DateTime.Now}
            });

            context.SaveChanges();

            context.AccLeaders.AddRange(new List<AccLeader>()
            {
                new AccLeader{ LeaderboardId = 1, AccountId = 1, Score = 200},
                new AccLeader{ LeaderboardId = 1, AccountId = 2, Score = 350},
                new AccLeader{ LeaderboardId = 1, AccountId = 3, Score = 100}

            });

            context.AppAccs.AddRange(new List<AppAcc>()
            {
                new AppAcc{ AccountId = 1, ApplicationId = 1, IsAdmin = true },
                new AppAcc{ AccountId = 2, ApplicationId = 1, IsAdmin = false},
                new AppAcc{ AccountId = 3, ApplicationId = 1, IsAdmin = false},
            });

            context.AppAches.AddRange(new List<AppAch>
            {
                new AppAch{ ApplicationId = 1, AchievmentId = 1},
                new AppAch{ ApplicationId = 1, AchievmentId = 2},
                new AppAch{ ApplicationId = 1, AchievmentId = 3}
            });

            context.AccAches.AddRange(new List<AccAch>
            {
                new AccAch{ AccountId = 1, AchievmentId = 1, Reached = true},
                new AccAch{ AccountId = 1, AchievmentId = 2, Reached = true},
                new AccAch{ AccountId = 1, AchievmentId = 3, Reached = false},
                new AccAch{ AccountId = 2, AchievmentId = 1, Reached = false},
                new AccAch{ AccountId = 2, AchievmentId = 2, Reached = false},
                new AccAch{ AccountId = 2, AchievmentId = 3, Reached = false},
                new AccAch{ AccountId = 3, AchievmentId = 1, Reached = true},
                new AccAch{ AccountId = 3, AchievmentId = 2, Reached = true},
                new AccAch{ AccountId = 3, AchievmentId = 3, Reached = true},
            });

            context.AppAccSaves.AddRange(new List<AppAccSave>
            {
                new AppAccSave{ CloudSaveId = 1, ApplicationId = 1, AccountId = 1},
                new AppAccSave{ CloudSaveId = 2, ApplicationId = 1, AccountId = 3}
            });

            context.SaveChanges();
        }
    }
}
