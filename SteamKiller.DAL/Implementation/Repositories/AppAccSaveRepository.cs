using Microsoft.EntityFrameworkCore;
using SteamKiller.DAL.EntitiesFramefork;
using SteamKiller.DAL.Entities;
using SteamKiller.DAL.Entities.Links;
using SteamKiller.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Repositories
{
    public class AppAccSaveRepository : IAppAccSaveRepository
    {
        DbSet<AppAccSave> AppAccSaves;

        public AppAccSaveRepository(ApplicationContext context)
        {
            AppAccSaves = context.AppAccSaves;
        }

        public async Task<bool> AddAsync(AppAccSave item)
        {
            await AppAccSaves.AddAsync(item);

            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<AppAccSave> entries)
        {
            await AppAccSaves.AddRangeAsync(entries);

            return true;
        }

        public async Task<bool> ContainsAsync(int id)
        {
            if (await AppAccSaves.AsNoTracking().AnyAsync(e => e.Id == id))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            AppAccSave save = await AppAccSaves.FirstOrDefaultAsync(e => e.Id == id);

            if (save != null)
            {
                AppAccSaves.Remove(save);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<AppAccSave, bool>> predicate)
        {
            IEnumerable<AppAccSave> entities = await FindAsync(predicate);

            if (entities.Count() > 0)
            {
                AppAccSaves.RemoveRange(entities);
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(AppAccSave item)
        {
            AppAccSave save = await AppAccSaves.FirstOrDefaultAsync(e => e.Id == item.Id);

            if (save != null)
            {
                save.AccountId = item.AccountId;
                save.ApplicationId = item.ApplicationId;
                save.CloudSaveId = item.CloudSaveId;

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<AppAccSave>> FindAsync(Expression<Func<AppAccSave, bool>> predicate)
        {
            return await AppAccSaves.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<AppAccSave> FindByIdAsync(int id)
        {
            return await AppAccSaves.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<AppAccSave> FindFirstAsync(Expression<Func<AppAccSave, bool>> predicate)
        {
            return await AppAccSaves.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<AppAccSave>> GetAllAsync()
        {
            return await AppAccSaves.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<CloudSave>> GetUserSaveGames(int accId, int appId)
        {
            return await AppAccSaves.Where(e => e.AccountId == accId && e.ApplicationId == appId)
                .Include(e => e.CloudSave)
                .Select(e => e.CloudSave)
                .AsNoTracking().ToListAsync();
        }
    }
}
