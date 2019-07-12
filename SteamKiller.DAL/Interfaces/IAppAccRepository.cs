using SteamKiller.DAL.Entites;
using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Entities.Complex;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Interfaces
{
    public interface IAppAccRepository:IRepository<AppAcc>
    {
        Task<IEnumerable<AccountComplex>> GetByApplication(int appId);
        Task<bool> CheckIfAccountHaveApplication(int appId, int accId);
        Task<IEnumerable<ApplicationComplex>> GetUserApplications(int accId);
        Task<AppAcc> GetUserApplication(int appId, int accId);
        Task<IEnumerable<int>> GetApplicationUserIds(int appId);
        Task<IEnumerable<ApplicationComplex>> SearchApplication(int accId, string query);
    }
}
