using SteamKiller.DAL.Entites;
using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Entities;
using SteamKiller.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.EntitiesFramefork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext context;

        public UnitOfWork(ApplicationContext _context)
        {
            context = _context;
        }

        public async Task<int> CreateApplication(Application _app, int accId)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    await context.Applications.AddAsync(_app);

                    if (!await SaveAsync())
                        throw new Exception();


                    await context.AppAccs.AddAsync(new AppAcc { AccountId = accId, ApplicationId = _app.Id, IsAdmin = true });

                    if (!await SaveAsync())
                        throw new Exception();

                    transaction.Commit();

                    return _app.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return -3;
                }
            }
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
