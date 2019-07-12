using SteamKiller.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.BLL.Services.Interfaces
{
    public interface IFinanceService
    {
        Task<int> AddFinanceProfile(FinanceProfileDTO finDTO);
        Task<bool> UpdateFinanceProfile(FinanceProfileDTO finDTO);
        Task<bool> RemoveFinanceProfile(int id);
        Task<FinanceProfileDTO> GetAccountFinanceProfile(int id);
    }
}
