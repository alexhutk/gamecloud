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
using System.Security.Claims;

namespace SteamKiller.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private DbSet<Account> Accounts;

        public AccountRepository(ApplicationContext context)
        {
            Accounts = context.Accounts;
        }

        public async Task<bool> AddAsync(Account item)
        {
            if (await Accounts.AnyAsync(e => e.Name == item.Name))
                return false;

            await Accounts.AddAsync(item);

            return true;
        }

        public async Task<bool> ContainsAsync(int id)
        {
            if (await Accounts.AsNoTracking().AnyAsync(e => e.Id == id))
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Account acc = await Accounts.FirstOrDefaultAsync(e=>e.Id == id);

            if (acc != null)
            {
                Accounts.Remove(acc);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Account, bool>> predicate)
        {
            IEnumerable<Account> entities = await FindAsync(predicate);

            if (entities.Count() > 0)
            {
                Accounts.RemoveRange(entities);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Account>> FindAsync(Expression<Func<Account, bool>> predicate)
        {
            return await Accounts.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<Account> FindByIdAsync(int id)
        {
            return await Accounts.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Account> FindFirstAsync(Expression<Func<Account, bool>> predicate)
        {
            return await Accounts.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await Accounts.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateAsync(Account item)
        {
            List<Account> acc = await Accounts.Where(l => l.Id == item.Id || l.Name == item.Name).ToListAsync();

            if (acc != null && acc.Count == 1)
            {
                acc[0].Name = item.Name;

                if (item.Password != null)
                {
                    acc[0].Password = item.Password;
                }

                if (item.Avatar != null)
                {
                    acc[0].Avatar = item.Avatar;
                }

                return true;
            }

            return false;
        }

        public async Task<int> Login(string name, string password)
        {
            Account acc = await Accounts.FirstOrDefaultAsync(e => e.Name == name && e.Password == password);

            if (acc != null)
            {
                return acc.Id;
            }

            return -1;
        }
    }
}
