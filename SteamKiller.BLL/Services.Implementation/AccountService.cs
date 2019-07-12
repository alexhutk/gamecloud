using Microsoft.EntityFrameworkCore;
using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Interfaces;
using SteamKiller.DAL.Interfaces;
using SteamKiller.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SteamKiller.BLL.Entities.Enums;
using SteamKiller.DAL.Entites.Links;
using System.Reflection;
using SteamKiller.DAL.Entities.Complex;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.BLL.Services.Implementation;

namespace SteamKiller.BLL.Services
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork unitOfWork;
        private IAccountRepository accRerpository;
        private IAppAccRepository appAccRepository;
        private ISecurityService secService;
        private IResourceService resService;

        public AccountService(IUnitOfWork _unit, IAccountRepository acR, IAppAccRepository acpP, ISecurityService _sec, IResourceService _res)
        {
            unitOfWork = _unit;
            accRerpository = acR;
            appAccRepository = acpP;
            secService = _sec;
            resService = _res;
        }

        public async Task<int> AddAccount(AccountDTO item)
        {
            Account newAcc = new Account { Name = item.Name, Password = secService.GeneratePasswordHash(item.Password), Avatar=resService.GetDefaultAvatar() };

            if (await accRerpository.AddAsync(newAcc))
            {
                if(await unitOfWork.SaveAsync())
                    return newAcc.Id;
            }

            return -1;
        }

        public async Task<bool> UpdateAccount(AccountDTO item)
        {
            Account updtAcc = new Account {Id = item.Id, Name = item.Name };

            if (item.Password != null)
            {
                updtAcc.Password = secService.GeneratePasswordHash(item.Password);
            }

            if (item.AvatarFile != null)
            {
                string avatarPath = await resService.SaveAvatar(item.AvatarFile, item.Id.ToString());

                updtAcc.Avatar = avatarPath;
            }

            if (await accRerpository.UpdateAsync(updtAcc))
            {
                if(await unitOfWork.SaveAsync())
                    return true;
            }

            return false;
        }

        public async Task<bool> RemoveAccount(int id)
        {
            if (await accRerpository.DeleteAsync(id))
            {
                if(await unitOfWork.SaveAsync())
                    return true;
            }

            return false;
        }

        public async Task<AccountCollectionDTO> GetAll()
        {
            AccountCollectionDTO accDto = new AccountCollectionDTO();
            IEnumerable<Account> accounts = await accRerpository.GetAllAsync();

            foreach (var child in accounts)
            {
                AccountDTO acc = new AccountDTO();

                acc.Id = child.Id;
                acc.Name = child.Name;
                acc.Password = child.Password;

                accDto.Accounts.Add(acc);
            }

            return accDto;
        }

        public async Task<AccountCollectionDTO> GetByApplication(int appId)
        {
            AccountCollectionDTO accDTO = new AccountCollectionDTO();
            IEnumerable<AccountComplex> accounts = await appAccRepository.GetByApplication(appId);

            foreach(var child in accounts)
            {
                AccountDTO acc = new AccountDTO();

                acc.Id = child.Id;
                acc.Name = child.Name;
                acc.Password = child.Password;
                acc.Permission = Permission.Admin;
                acc.PermissionValue = child.IsAdmin;

                accDTO.Accounts.Add(acc);
            }

            return accDTO;
        }

        public async Task<AccountDTO> GetById(int id)
        {
            Account acc = await accRerpository.FindByIdAsync(id);

            if (acc != null)
                return new AccountDTO { Id = acc.Id, Name = acc.Name, Password = acc.Password, Avatar = acc.Avatar };

            return null;
        }

        public async Task<AccountDTO> GetByName(string name)
        {
            IEnumerable<Account> acc = await accRerpository.FindAsync(e=>e.Name == name);

            if (acc != null && acc.Count() > 0)
                return new AccountDTO { Id = acc.First().Id, Name = acc.First().Name, Password = acc.First().Password, Avatar = acc.First().Avatar };

            return null;
        }

        public async Task<AuthorizedAccountDTO> Login(AccountDTO accDTO, string type)
        {
            Account acc = await accRerpository.FindFirstAsync(e => e.Name == accDTO.Name);

            if (acc == null)
            {
                return null;
            }

            if (secService.VerifyPassword(accDTO.Password, acc.Password))
            {
                var authAcc = new AuthorizedAccountDTO();

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, accDTO.Name),
                    new Claim("Id", acc.Id.ToString()),
                };

                authAcc.Identity = new ClaimsIdentity(claims, type, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                authAcc.Avatar = acc.Avatar;

                return authAcc;
            }

            return null;
        }

        public async Task<bool> CheckPermission(AccountDTO accDTO, List<Permission> perms = null)
        {
            if (perms != null)
            {
                IEnumerable<Permission> permList = await GetUserPermissions(accDTO);

                foreach (var c in permList)
                {
                    foreach (var p in perms)
                    {
                        if (c == p)
                            return true;
                    }
                }
            }
            else
            {
                return await appAccRepository.CheckIfAccountHaveApplication(accDTO.ApplicationID, accDTO.Id);
            }

            return false;
        }

        public async Task<List<Permission>> GetUserPermissions(AccountDTO accDTO)
        {
            AppAcc appAcc = await appAccRepository.FindFirstAsync(e => e.AccountId == accDTO.Id && e.ApplicationId == accDTO.ApplicationID);
            List<Permission> perms = new List<Permission>();

            if (appAcc != null && appAcc.IsAdmin)
                perms.Add(Permission.Admin);

            return perms;
        }

        public async Task<bool> SetUserPermission(AccountDTO accDTO)
        {
            AppAcc appAcc = new AppAcc();
            appAcc.AccountId = accDTO.Id;
            appAcc.ApplicationId = accDTO.ApplicationID;

            if (accDTO.Permission == Permission.Admin)
                appAcc.IsAdmin = accDTO.PermissionValue;

            if (!await appAccRepository.UpdateAsync(appAcc))
                return false;

            if (await unitOfWork.SaveAsync())
                return true;

            return false;
        }

        public async Task<bool> SetUserPermissionByName(AccountDTO accDTO)
        {
            AppAcc appAcc = new AppAcc();
            AccountDTO acc = await GetByName(accDTO.Name);
            appAcc.AccountId = acc.Id;
            appAcc.ApplicationId = accDTO.ApplicationID;

            if (accDTO.Permission == Permission.Admin)
                appAcc.IsAdmin = true;
            else
                appAcc.IsAdmin = false;

            if (!await appAccRepository.UpdateAsync(appAcc))
                return false;

            if (await unitOfWork.SaveAsync())
                return true;

            return false;
        }
    }
}
