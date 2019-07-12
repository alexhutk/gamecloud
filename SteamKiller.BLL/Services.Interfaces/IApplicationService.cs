using System;
using System.Collections.Generic;
using System.Text;
using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Entites;
using System.Threading.Tasks;
using SteamKiller.BLL.Entities;

namespace SteamKiller.BLL.Interfaces
{
    public interface IApplicationService
    {
        Task<int> AddApplication(ApplicationDTO app, int accId);
        Task<bool> AddApplicationToAccount(int appId, int accId);
        Task<bool> UpdateApplication(ApplicationDTO app);
        Task<bool> RemoveApplication(int id);
        Task<bool> RemoveApplicationFromAccount(int appId, int accId);
        Task<ApplicationDTO> GetApplication(int id);
        Task<ApplicationCollectionDTO> GetAllApplications();
        Task<ApplicationCollectionDTO> GetUserApplications(int accId);
    }
}
