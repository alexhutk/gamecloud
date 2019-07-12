using Microsoft.EntityFrameworkCore;
using SteamKiller.DAL.EntitiesFramefork;
using SteamKiller.DAL.Entities.Complex;
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
    public class AccAchRepository : IAccAchRepository
    {
        DbSet<AccAch> AccAches;

        public AccAchRepository(ApplicationContext context)
        {
            AccAches = context.AccAches;
        }

        public async Task<bool> AddAsync(AccAch item)
        {
            await AccAches.AddAsync(item);

            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<AccAch> entries)
        {
            await AccAches.AddRangeAsync(entries);

            return true;
        }

        public async Task<bool> ContainsAsync(int id)
        {
            if (await AccAches.AsNoTracking().AnyAsync(e => e.Id == id))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            AccAch ach = await AccAches.FirstOrDefaultAsync(e => e.Id == id);

            if (ach != null)
            {
                AccAches.Remove(ach);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<AccAch, bool>> predicate)
        {
            IEnumerable<AccAch> entities = await FindAsync(predicate);

            if (entities.Count() > 0)
            {
                AccAches.RemoveRange(entities);
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(AccAch item)
        {
            AccAch ach = await AccAches.FirstOrDefaultAsync(e => e.AccountId == item.AccountId && e.AchievmentId == item.AchievmentId);

            if (ach != null)
            {
                ach.AccountId = item.AccountId;
                ach.AchievmentId = item.AchievmentId;
                ach.Reached = item.Reached;

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<AccAch>> FindAsync(Expression<Func<AccAch, bool>> predicate)
        {
            return await AccAches.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<AccAch> FindByIdAsync(int id)
        {
            return await AccAches.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<AccAch> FindFirstAsync(Expression<Func<AccAch, bool>> predicate)
        {
            return await AccAches.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<AccAch>> GetAllAsync()
        {
            return await AccAches.AsNoTracking().ToListAsync();
        }

        public async Task<List<AchievmentComplex>> GetUserAchievments(int appId, int accId)
        {
            return await AccAches.Where(e => e.AccountId == accId)
                .Include(e => e.Achievment)
                .ThenInclude(e => e.AppAches)
                .SelectMany(a => a.Achievment.AppAches,
                (a, e) => new AchievmentComplex
                {
                    Id = a.AchievmentId,
                    Name = a.Achievment.Name,
                    Description = a.Achievment.Description,
                    Reached = a.Reached,
                    ApplicationId = e.ApplicationId,
                    ImageData = a.Achievment.ImageData,
                    ImageMimeType = a.Achievment.ImageMimeType
                })
                .Where(e => e.ApplicationId == appId)
                .ToListAsync();
        }

        public async Task<List<AccAch>> GetAchievmentSummaryByApplication(int appId, int achId)
        {
            return await AccAches
                .Where(e=>e.AchievmentId == achId)
                .Include(e => e.Achievment)
                .ThenInclude(e => e.AppAches)
                .SelectMany(a => a.Achievment.AppAches,
                (a, e) => new
                {
                    Id = a.AchievmentId,
                    AccountId = a.AccountId,
                    Reached = a.Reached,
                    ApplicationId = e.ApplicationId
                })
                .Where(e => e.ApplicationId == appId)
                .Select(e => new AccAch
                {
                    Id = e.Id,
                    AccountId = e.AccountId,
                    Reached = e.Reached
                })
                .ToListAsync();
        }
    }
}
