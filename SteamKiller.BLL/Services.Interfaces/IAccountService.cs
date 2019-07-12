using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SteamKiller.BLL.Entities;
using System.Security.Claims;
using SteamKiller.BLL.Entities.Enums;

namespace SteamKiller.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<int> AddAccount(AccountDTO item);
        Task<bool> UpdateAccount(AccountDTO item);
        Task<bool> RemoveAccount(int id);
        Task<AccountCollectionDTO> GetAll();
        Task<AccountCollectionDTO> GetByApplication(int appId);
        Task<AccountDTO> GetById(int id);
        Task<AccountDTO> GetByName(string name);
        Task<AuthorizedAccountDTO> Login(AccountDTO accDTO, string type);
        Task<bool> CheckPermission(AccountDTO accDTO, List<Permission> perms = null);
        Task<List<Permission>> GetUserPermissions(AccountDTO accDTO);
        Task<bool> SetUserPermission(AccountDTO accDTO);
        Task<bool> SetUserPermissionByName(AccountDTO accDTO);
    }
}
