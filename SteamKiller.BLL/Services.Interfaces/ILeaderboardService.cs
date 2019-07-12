using System;
using System.Collections.Generic;
using System.Text;
using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Entites;
using System.Threading.Tasks;
using SteamKiller.BLL.Entities;

namespace SteamKiller.BLL.Interfaces
{
    public interface ILeaderboardService
    {
        Task<long> AddScoreToLeaderboard(AccLeaderDTO accDTO);
        Task<int> AddLeaderboard(int appId, string name);
        Task<bool> DeleteLeaderboard(int appId);
        Task<string> GetLeaderboardNameAsync(int id);
        Task<IEnumerable<LeaderboardEntryDTO>> GetLeaderboardEntriesAsync(int id);
        Task<LeaderboardSignatureDTO> GetLeaderboardsByAppId(int appId);
        Task<LeaderboardDTO> GetLeaderboardById(int id);
    }
}
