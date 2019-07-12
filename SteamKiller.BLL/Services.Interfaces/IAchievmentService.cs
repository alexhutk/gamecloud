using SteamKiller.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.BLL.Services.Interfaces
{
    public interface IAchievmentService
    {
        Task<int> AddAchievment(AchievmentDTO achDTO);
        Task<bool> UpdateAchievment(AchievmentDTO achDTO);
        Task<bool> RemoveAchievment(AchievmentDTO achDTO);
        Task<AchievmentCollectionDTO> GetUserAchievments(int appId, int accId);
        Task<AchievmentCollectionDTO> GetApplicationAchievments(int appId);
        Task<AchievmentCollectionDTO> GetAchievmentSummaryByApplication(int appId, int achId);
        Task<bool> SetAchievmentReached(int achId, int accId);
        Task<bool> SetAchievmentUnreached(int achId, int accId);
    }
}
