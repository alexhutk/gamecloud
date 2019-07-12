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
    public class CloudSaveRepository : ICloudSaveRepository
    {
        DbSet<CloudSave> CloudSaves;

        public CloudSaveRepository(ApplicationContext context)
        {
            CloudSaves = context.CloudSaves;
        }

        public async Task<bool> AddAsync(CloudSave item)
        {
            await CloudSaves.AddAsync(item);

            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<CloudSave> entries)
        {
            await CloudSaves.AddRangeAsync(entries);

            return true;
        }

        public async Task<bool> ContainsAsync(int id)
        {
            if (await CloudSaves.AsNoTracking().AnyAsync(e => e.Id == id))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            CloudSave save = await CloudSaves.FirstOrDefaultAsync(e => e.Id == id);

            if (save != null)
            {
                CloudSaves.Remove(save);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<CloudSave, bool>> predicate)
        {
            IEnumerable<CloudSave> entities = await FindAsync(predicate);

            if (entities.Count() > 0)
            {
                CloudSaves.RemoveRange(entities);
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(CloudSave item)
        {
            CloudSave save = await CloudSaves.FirstOrDefaultAsync(e => e.Id == item.Id);

            if (save != null)
            {
                save.SaveData = item.SaveData;
                save.SaveTime = item.SaveTime;

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<CloudSave>> FindAsync(Expression<Func<CloudSave, bool>> predicate)
        {
            return await CloudSaves.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<CloudSave> FindByIdAsync(int id)
        {
            return await CloudSaves.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<CloudSave> FindFirstAsync(Expression<Func<CloudSave, bool>> predicate)
        {
            return await CloudSaves.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<CloudSave>> GetAllAsync()
        {
            return await CloudSaves.AsNoTracking().ToListAsync();
        }
    }
}
