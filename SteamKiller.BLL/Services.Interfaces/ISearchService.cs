using System;
using System.Collections.Generic;
using System.Text;
using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Entites;
using System.Threading.Tasks;
using SteamKiller.BLL.Entities;

namespace SteamKiller.BLL.Services.Interfaces
{
    public interface ISearchService
    {
        Task<ApplicationCollectionDTO> SearchApplication(int accId, string query);
    }
}
