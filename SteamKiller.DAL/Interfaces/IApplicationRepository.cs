using SteamKiller.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Interfaces
{
    public interface IApplicationRepository:IRepository<Application>
    {
        Task<Leaderboard> GetApplicationLeaderboard(int appId);
    }
}
