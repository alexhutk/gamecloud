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
    public class AppAchRepository : IAppAchRepository
    {
        DbSet<AppAch> AppAches;

        public AppAchRepository(ApplicationContext context)
        {
            AppAches = context.AppAches;
        }

        public async Task<bool> AddAsync(AppAch item)
        {
            await AppAches.AddAsync(item);

            return true;
        }

        public async Task<bool> ContainsAsync(int id)
        {
            if (await AppAches.AsNoTracking().AnyAsync(e => e.Id == id))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            AppAch ach = await AppAches.FirstOrDefaultAsync(e => e.Id == id);

            if (ach != null)
            {
                AppAches.Remove(ach);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<AppAch, bool>> predicate)
        {
            IEnumerable<AppAch> entities = await FindAsync(predicate);

            if (entities.Count() > 0)
            {
                AppAches.RemoveRange(entities);
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(AppAch item)
        {
            AppAch ach = await AppAches.FirstOrDefaultAsync(e => e.Id == item.Id);

            if (ach != null)
            {
                ach.AchievmentId = item.AchievmentId;
                ach.ApplicationId = item.ApplicationId;

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<AppAch>> FindAsync(Expression<Func<AppAch, bool>> predicate)
        {
            return await AppAches.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<AppAch> FindByIdAsync(int id)
        {
            return await AppAches.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<AppAch> FindFirstAsync(Expression<Func<AppAch, bool>> predicate)
        {
            return await AppAches.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<AppAch>> GetAllAsync()
        {
            return await AppAches.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Achievment>> GetAchievmentsByAppId(int appId)
        {
            return await AppAches.AsNoTracking().Include(e => e.Achievment).Where(e => e.ApplicationId == appId).Select(e => e.Achievment).ToListAsync();
        }
    }
}
