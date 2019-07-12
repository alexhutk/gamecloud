using SteamKiller.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Interfaces
{
    public interface ILeaderboardRepository:IRepository<Leaderboard>
    {
        Task<Leaderboard> GetLeaderboardWithAccountInfoById(int id);
    }
}
