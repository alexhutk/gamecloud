using SteamKiller.DAL.Entities;
using SteamKiller.DAL.Entities.Links;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Interfaces
{
    public interface IAppAchRepository:IRepository<AppAch>
    {
        Task<IEnumerable<Achievment>> GetAchievmentsByAppId(int appId);
    }
}
