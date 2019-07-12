using Microsoft.EntityFrameworkCore;
using SteamKiller.DAL.Entities;
using SteamKiller.DAL.EntitiesFramefork;
using SteamKiller.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Implementation.Repositories
{
    public class FinanceProfileRepository : IFinanceProfileRepository
    {
        DbSet<FinanceProfile> Profiles;

        public FinanceProfileRepository(ApplicationContext context)
        {
            Profiles = context.FinanceProfiles;
        }

        public async Task<bool> AddAsync(FinanceProfile item)
        {
            try
            {
                await Profiles.AddAsync(item);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(FinanceProfile item)
        {
            FinanceProfile oldProfile = await Profiles.FirstOrDefaultAsync(e => e.Id == item.Id);

            if (item != null && oldProfile != null)
            {
                oldProfile.Address = item.Address;
                oldProfile.BankName = item.BankName;
                oldProfile.IbanNumber = item.IbanNumber;

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            FinanceProfile oldProfile = await Profiles.FirstOrDefaultAsync(e => e.Id == id);

            if (oldProfile != null)
            {
                Profiles.Remove(oldProfile);

                return true;
            }

            return false;
        }

        public async Task<FinanceProfile> FindByIdAsync(int id)
        {
            FinanceProfile profile = await Profiles.AsNoTracking().FirstOrDefaultAsync(e => e.AccountId == id);

            return profile;
        }

        public Task<bool> ContainsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Expression<Func<FinanceProfile, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FinanceProfile>> FindAsync(Expression<Func<FinanceProfile, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<FinanceProfile> FindFirstAsync(Expression<Func<FinanceProfile, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FinanceProfile>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

    }
}
