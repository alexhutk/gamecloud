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
    public class LeaderboardRepository : ILeaderboardRepository
    {
        DbSet<Leaderboard> Leaderboards;

        public LeaderboardRepository(ApplicationContext context)
        {
            Leaderboards = context.Leaderboards;
        }

        public async Task<bool> AddAsync(Leaderboard item)
        {
            if (item != null)
            {
                await Leaderboards.AddAsync(item);
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Leaderboard leader = await Leaderboards.FindAsync(id);

            if (leader != null)
            {
                Leaderboards.Remove(leader);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Leaderboard, bool>> predicate)
        {
            IEnumerable<Leaderboard> entities = await FindAsync(predicate);

            if (entities.Count() > 0)
            {
                Leaderboards.RemoveRange(entities);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Leaderboard>> FindAsync(Expression<Func<Leaderboard, bool>> predicate)
        {
            return await Leaderboards.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<Leaderboard> FindFirstAsync(Expression<Func<Leaderboard, bool>> predicate)
        {
            return await Leaderboards.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Leaderboard>> GetAllAsync()
        {
            return await Leaderboards.AsNoTracking().ToListAsync();
        }

        public async Task<Leaderboard> GetLeaderboardWithAccountInfoById(int id)
        {
            return await Leaderboards.Where(e => e.Id == id).Include(e => e.AccLeaders).ThenInclude(e => e.Account).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Leaderboard> FindByIdAsync(int id)
        {
            return await Leaderboards.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<bool> UpdateAsync(Leaderboard item)
        {
            if (item != null)
            {
                Leaderboards.Update(item);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public async Task<bool> ContainsAsync(int id)
        {
            if (await Leaderboards.AnyAsync(e => e.Id == id))
                return true;
            else
                return false;
        }
    }
}
