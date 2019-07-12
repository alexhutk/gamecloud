using Microsoft.EntityFrameworkCore.Query;
using SteamKiller.DAL.Entites;
using SteamKiller.DAL.Entites.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.DAL.Interfaces
{
    public interface IAccLeaderRepository:IRepository<AccLeader>
    {
        IIncludableQueryable<AccLeader, Account> GetLeaderboardEntriesById(int id);
    }
}
