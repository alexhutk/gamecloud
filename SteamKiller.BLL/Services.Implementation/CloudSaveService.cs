using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.DAL.Entities;
using SteamKiller.DAL.Entities.Links;
using SteamKiller.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.BLL.Services.Implementation
{
    public class CloudSaveService : ICloudSaveService
    {
        private IUnitOfWork unitOfWork;
        private ICloudSaveRepository saveRepository;
        private IAppAccSaveRepository appAccSaveRepository;
        private IApplicationRepository appRepository;
        private IAccountRepository accRepository;

        public CloudSaveService(IUnitOfWork _u, ICloudSaveRepository _save, IAppAccSaveRepository _appAccSave, IApplicationRepository _app, IAccountRepository _acc)
        {
            unitOfWork = _u;
            saveRepository = _save;
            appAccSaveRepository = _appAccSave;
            appRepository = _app;
            accRepository = _acc;
        }

        public async Task<SaveGameCollectionDTO> GetUserSaveGames(int accId, int appId)
        {
            SaveGameCollectionDTO saveDTO = new SaveGameCollectionDTO();
            IEnumerable<CloudSave> saves = await appAccSaveRepository.GetUserSaveGames(accId, appId);

            foreach (var c in saves)
            {
                saveDTO.EntryList.Add(new SaveGameDTO
                {
                    Id = c.Id,
                    SaveData = c.SaveData,
                    SaveTime = c.SaveTime
                });
            }

            return saveDTO;
        }

        public async Task<bool> RemoveSaveGame(int id)
        {
            if (await saveRepository.DeleteAsync(id))
            {
                if (await unitOfWork.SaveAsync())
                    return true;
            }

            return false;
        }

        public async Task<int> SaveGame(SaveGameDTO saveDTO)
        {
            CloudSave save = new CloudSave();

            save.Id = saveDTO.Id;
            save.SaveData = saveDTO.SaveData;
            save.SaveTime = saveDTO.SaveTime;

            if (saveDTO.Id != 0)
            {
                if (!await saveRepository.UpdateAsync(save))
                    return -1;
            }
            else
            {
                if (!await appRepository.ContainsAsync(saveDTO.ApplicationId) || !await accRepository.ContainsAsync(saveDTO.AccountId))
                    return -4;

                if (!await saveRepository.AddAsync(save))
                    return -1;

                AppAccSave accSave = new AppAccSave
                {
                    AccountId = saveDTO.AccountId,
                    ApplicationId = saveDTO.ApplicationId,
                    CloudSaveId = save.Id
                };

                if (!await appAccSaveRepository.AddAsync(accSave))
                    return -2;
            }

            if (await unitOfWork.SaveAsync())
                return save.Id;

            return -3;
        }
    }
}
