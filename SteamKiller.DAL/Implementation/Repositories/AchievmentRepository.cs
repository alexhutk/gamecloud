using Microsoft.EntityFrameworkCore;
using SteamKiller.DAL.EntitiesFramefork;
using SteamKiller.DAL.Entities;
using SteamKiller.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Repositories
{
    public class AchievmentRepository : IAchievmentRepository
    {
        DbSet<Achievment> Achievments;

        public AchievmentRepository(ApplicationContext context)
        {
            Achievments = context.Achievments;
        }

        public async Task<bool> AddAsync(Achievment item)
        {
            await Achievments.AddAsync(item);

            return true;
        }

        public async Task<bool> ContainsAsync(int id)
        {
            if (await Achievments.AsNoTracking().AnyAsync(e => e.Id == id))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Achievment ach = await Achievments.FirstOrDefaultAsync(e => e.Id == id);

            if (ach != null)
            {
                Achievments.Remove(ach);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Achievment, bool>> predicate)
        {
            IEnumerable<Achievment> entities = await FindAsync(predicate);

            if (entities.Count() > 0)
            {
                Achievments.RemoveRange(entities);
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Achievment item)
        {
            Achievment ach = await Achievments.FirstOrDefaultAsync(e => e.Id == item.Id);

            if (ach != null)
            {
                ach.Name = item.Name;
                ach.Description = item.Description;
                ach.ImageData = item.ImageData;
                ach.ImageMimeType = item.ImageMimeType;

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Achievment>> FindAsync(Expression<Func<Achievment, bool>> predicate)
        {
            return await Achievments.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<Achievment> FindByIdAsync(int id)
        {
            return await Achievments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Achievment> FindFirstAsync(Expression<Func<Achievment, bool>> predicate)
        {
            return await Achievments.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Achievment>> GetAllAsync()
        {
            return await Achievments.AsNoTracking().ToListAsync();
        }
    }
}
