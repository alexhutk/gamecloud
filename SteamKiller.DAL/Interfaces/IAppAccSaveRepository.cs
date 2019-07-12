using SteamKiller.DAL.Entities;
using SteamKiller.DAL.Entities.Links;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Interfaces
{
    public interface IAppAccSaveRepository : IRepository<AppAccSave>
    {
        Task<IEnumerable<CloudSave>> GetUserSaveGames(int accId, int appId);
    }
}
