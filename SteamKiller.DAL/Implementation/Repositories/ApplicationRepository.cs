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

namespace SteamKiller.DAL.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        DbSet<Application> Applications;

        public ApplicationRepository(ApplicationContext context)
        {
            Applications = context.Applications;
        }

        public async Task<bool> AddAsync(Application item)
        {
            await Applications.AddAsync(item);

            return true;
        }

        public async Task<bool> ContainsAsync(int id)
        {
            if (await Applications.AsNoTracking().AnyAsync(e => e.Id == id))
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Application app = await Applications.FindAsync(id);

            if (app != null)
            {
                Applications.Remove(app);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Application, bool>> predicate)
        {
            IEnumerable<Application> entities = await FindAsync(predicate);

            if (entities.Count() > 0)
            {
                Applications.RemoveRange(entities);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Application>> FindAsync(Expression<Func<Application, bool>> predicate)
        {
            return await Applications.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<Application> FindByIdAsync(int id)
        {
            return await Applications.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Application> FindFirstAsync(Expression<Func<Application, bool>> predicate)
        {
            return await Applications.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Application>> GetAllAsync()
        {
            return await Applications.AsNoTracking().ToListAsync();
        }

        public async Task<Leaderboard> GetApplicationLeaderboard(int appId)
        {
            return await Applications.Where(e => e.Id == appId).Include(e => e.Leaderboard).Select(e => e.Leaderboard).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(Application item)
        {
            Application oldApp = await Applications.FirstOrDefaultAsync(e => e.Id == item.Id);

            if (item != null && oldApp != null)
            {
                oldApp.Name = item.Name;

                if (item.ImageData != null)
                {
                    oldApp.ImageData = item.ImageData;
                    oldApp.ImageMimeType = item.ImageMimeType;
                }

                return true;
            }

            return false;
        }
    }
}
