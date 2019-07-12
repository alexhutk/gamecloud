using SteamKiller.DAL.EntitiesFramefork;
using SteamKiller.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CreateApplication(Application app, int accId);
        Task<bool> SaveAsync();
    }
}
