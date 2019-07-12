using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SteamKiller.DAL.Interfaces;
using SteamKiller.DAL.Entites;
using SteamKiller.DAL.EntitiesFramefork;
using System.Linq.Expressions;
using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Entities.Complex;

namespace SteamKiller.DAL.Repositories
{
    public class AppAccRepository : IAppAccRepository
    {
        DbSet<AppAcc> AppAccs;

        public AppAccRepository(ApplicationContext context)
        {
            AppAccs = context.AppAccs;
        }

        public async Task<bool> AddAsync(AppAcc item)
        {
            await AppAccs.AddAsync(item);

            return true;
        }

        public async Task<bool> ContainsAsync(int id)
        {
            if (await AppAccs.AsNoTracking().AnyAsync(e => e.Id == id))
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            AppAcc acc = await AppAccs.FindAsync(id);

            if (acc != null)
            {
                AppAccs.Remove(acc);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<AppAcc, bool>> predicate)
        {
            IEnumerable<AppAcc> entities = await FindAsync(predicate);

            if (entities.Count() > 0)
            {
                AppAccs.RemoveRange(entities);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<AppAcc>> FindAsync(Expression<Func<AppAcc, bool>> predicate)
        {
            return await AppAccs.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<AppAcc> FindByIdAsync(int id)
        {
            return await AppAccs.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<AppAcc> FindFirstAsync(Expression<Func<AppAcc, bool>> predicate)
        {
            return await AppAccs.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<AppAcc>> GetAllAsync()
        {
            return await AppAccs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AccountComplex>> GetByApplication(int appId)
        {
            return await AppAccs.Where(e => e.ApplicationId == appId)
                .Include(e => e.Account)
                .Select(e => new AccountComplex{
                    Id = e.Account.Id,
                    Name = e.Account.Name,
                    Password = e.Account.Password,
                    ApplicationID = appId,
                    IsAdmin = e.IsAdmin
                })
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ApplicationComplex>> GetUserApplications(int accId)
        {
             return await AppAccs.Include(e => e.Application)
                .Where(e => e.AccountId == accId)
                .Select(e => new ApplicationComplex {
                    Id = e.Application.Id,
                    Name = e.Application.Name,
                    ImageData = e.Application.ImageData,
                    ImageMimeType = e.Application.ImageMimeType,
                    IsAdmin = e.IsAdmin
                })
                .ToListAsync();
        }

        public async Task<AppAcc> GetUserApplication(int appId, int accId)
        {
            return await AppAccs.Where(e => e.ApplicationId == appId && e.AccountId == accId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(AppAcc item)
        {
            AppAcc acc = await AppAccs.FirstOrDefaultAsync(l => l.AccountId == item.AccountId && l.ApplicationId == item.ApplicationId);

            if (acc != null)
            {
                acc.IsAdmin = item.IsAdmin;
                return true;
            }
            else
            {
                return await AddAsync(item);
            }
        }

        public async Task<bool> CheckIfAccountHaveApplication(int appId, int accId)
        {
            if (await AppAccs.Where(e => e.ApplicationId == appId && e.AccountId == accId).CountAsync() == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<int>> GetApplicationUserIds(int appId)
        {
            return await AppAccs.Where(e => e.ApplicationId == appId).AsNoTracking().Select(e => e.AccountId).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationComplex>> SearchApplication(int accId, string query)
        {
            return await AppAccs.Include(e => e.Application)
                .Where(e => EF.Functions.Like(e.Application.Name, $"%{query}%") && e.AccountId == accId)
                .Select(e => new ApplicationComplex
                {
                    Id = e.Application.Id,
                    Name = e.Application.Name,
                    ImageData = e.Application.ImageData,
                    ImageMimeType = e.Application.ImageMimeType,
                    IsAdmin = e.IsAdmin
                })
                .ToListAsync();
        }
    }
}
