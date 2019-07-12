using SteamKiller.DAL.Entities.Complex;
using SteamKiller.DAL.Entities.Links;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Interfaces
{
    public interface IAccAchRepository:IRepository<AccAch>
    {
        Task<bool> AddRangeAsync(IEnumerable<AccAch> entries);
        Task<List<AchievmentComplex>> GetUserAchievments(int appId, int accId);
        Task<List<AccAch>> GetAchievmentSummaryByApplication(int appId, int achId);
    }
}
