using SteamKiller.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Interfaces
{
    public interface IAccountRepository:IRepository<Account>
    {
        Task<int> Login(string name, string password);
    }
}
