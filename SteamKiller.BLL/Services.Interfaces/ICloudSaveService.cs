using SteamKiller.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.BLL.Services.Interfaces
{
    public interface ICloudSaveService
    {
        Task<int> SaveGame(SaveGameDTO saveDTO);
        Task<bool> RemoveSaveGame(int id);
        Task<SaveGameCollectionDTO> GetUserSaveGames(int accId, int appId);
    }
}
