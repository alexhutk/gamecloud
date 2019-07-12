using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SteamKiller.DAL.EntitiesFramefork;
using SteamKiller.DAL.Entites;
using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Repositories
{
    public class AccLeaderRepository : IAccLeaderRepository
    {
        DbSet<AccLeader> AccLeaders;

        public AccLeaderRepository(ApplicationContext _context)
        {
            AccLeaders = _context.AccLeaders;
        }

        public async Task<bool> AddAsync(AccLeader item)
        {
            if (item != null)
            {
                if (await AccLeaders.AnyAsync(e => e.LeaderboardId == item.LeaderboardId && e.AccountId == item.AccountId))
                    await UpdateAsync(item);
                else
                    await AccLeaders.AddAsync(item);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            AccLeader leader = await AccLeaders.FindAsync(id);

            if (leader != null)
            {
                AccLeaders.Remove(leader);
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<AccLeader, bool>> predicate)
        {
            List<AccLeader> entities = await AccLeaders.Where(predicate).ToListAsync();

            if (entities.Count > 0)
            {
                AccLeaders.RemoveRange(entities);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<AccLeader>> FindAsync(Expression<Func<AccLeader, bool>> predicate)
        {
            IEnumerable<AccLeader> l = await AccLeaders.AsNoTracking().Where(predicate).ToListAsync();
            return l;
        }

        public async Task<AccLeader> FindFirstAsync(Expression<Func<AccLeader, bool>> predicate)
        {
            return await AccLeaders.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<AccLeader>> GetAllAsync()
        {
            return await AccLeaders.AsNoTracking().ToListAsync();
        }

        public IIncludableQueryable<AccLeader, Account> GetLeaderboardEntriesById(int id)
        {
            return AccLeaders.Where(e => e.LeaderboardId == id).Include(e => e.Account);
        }

        public async Task<AccLeader> FindByIdAsync(int id)
        {
            return await AccLeaders.AsNoTracking().FirstOrDefaultAsync(e=>e.Id == id);
        }

        public async Task<bool> UpdateAsync(AccLeader item)
        {
            AccLeader leader = await AccLeaders.FirstOrDefaultAsync(e => e.LeaderboardId == item.LeaderboardId && e.AccountId == item.AccountId);

            if (leader != null)
            {
                leader.AccountId = item.AccountId;
                leader.LeaderboardId = item.LeaderboardId;
                leader.Score = item.Score;

                return true;
            }

            return false;
        }

        public async Task<bool> ContainsAsync(int id)
        {
            if (await AccLeaders.AnyAsync(e => e.Id == id))
                return true;
            else
                return false;
        }
    }
}
