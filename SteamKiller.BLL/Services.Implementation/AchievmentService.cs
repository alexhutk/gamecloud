using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.DAL.Entities;
using SteamKiller.DAL.Entities.Complex;
using SteamKiller.DAL.Entities.Links;
using SteamKiller.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.BLL.Services.Implementation
{
    public class AchievmentService : IAchievmentService
    {
        private IUnitOfWork unitOfWork;
        private IAchievmentRepository achRepository;
        private IAppAchRepository appAchRepository;
        private IAccAchRepository accAchRepository;
        private IAppAccRepository appAccRepository;

        public AchievmentService(IUnitOfWork _u, IAchievmentRepository _ach, IAppAchRepository _appAch, IAccAchRepository _acc, IAppAccRepository _appAcc)
        {
            unitOfWork = _u;
            achRepository = _ach;
            appAchRepository = _appAch;
            accAchRepository = _acc;
            appAccRepository = _appAcc;
        }

        public async Task<int> AddAchievment(AchievmentDTO achDTO)
        {
            Achievment ach = new Achievment();

            if(achDTO != null)
            {
                ach.Name = achDTO.Name;
                ach.Description = achDTO.Description;

                if (achDTO.ImageData != null)
                {
                    ach.ImageData = achDTO.ImageData;
                    ach.ImageMimeType = achDTO.ImageMimeType;
                }
            }

            if (!await achRepository.AddAsync(ach))
            {
                return -1;
            }

            AppAch appAch = new AppAch { ApplicationId = achDTO.ApplicationId, AchievmentId = ach.Id };

            if (!await appAchRepository.AddAsync(appAch))
            {
                return -2;
            }

            IEnumerable<int> accIds = await appAccRepository.GetApplicationUserIds(achDTO.ApplicationId);
            List<AccAch> accAches = new List<AccAch>();

            foreach (var child in accIds)
            {
                accAches.Add(new AccAch { AccountId = child, AchievmentId = ach.Id, Reached = false });
            }

            if (!await accAchRepository.AddRangeAsync(accAches))
                return -3;

            if (await unitOfWork.SaveAsync())
            {
                return ach.Id;
            }

            return -4;
        }

        public async Task<bool> UpdateAchievment(AchievmentDTO achDTO)
        {
            Achievment ach = new Achievment();

            ach.Id = achDTO.Id;
            ach.Name = achDTO.Name;
            ach.Description = achDTO.Description;
            ach.ImageData = achDTO.ImageData;
            ach.ImageMimeType = achDTO.ImageMimeType;

            if (await achRepository.UpdateAsync(ach))
            {
                if (await unitOfWork.SaveAsync())
                    return true;
            }

            return false;
        }

        public async Task<bool> RemoveAchievment(AchievmentDTO achDTO)
        {
            if (await achRepository.DeleteAsync(achDTO.Id))
            {
                if (await unitOfWork.SaveAsync())
                    return true;
            }

            return false;
        }

        public async Task<AchievmentCollectionDTO> GetAchievmentSummaryByApplication(int appId, int achId)
        {
            AchievmentCollectionDTO achDTO = new AchievmentCollectionDTO();

            IEnumerable<AccAch> achList = await accAchRepository.GetAchievmentSummaryByApplication(appId, achId);

            foreach (var child in achList)
            {
                achDTO.Entries.Add(new AchievmentDTO { AccountId = child.AccountId, Reached = child.Reached, Id = achId, ApplicationId = appId });
            }

            return achDTO;
        }

        public async Task<AchievmentCollectionDTO> GetApplicationAchievments(int appId)
        {
            AchievmentCollectionDTO achDTO = new AchievmentCollectionDTO();

            IEnumerable<Achievment> achList = await appAchRepository.GetAchievmentsByAppId(appId);

            foreach (var child in achList)
            {
                achDTO.Entries.Add(new AchievmentDTO { Id = child.Id, Name = child.Name, Description = child.Description, ImageData = child.ImageData, ImageMimeType = child.ImageMimeType });
            }

            return achDTO;
        }

        public async Task<AchievmentCollectionDTO> GetUserAchievments(int appId, int accId)
        {
            AchievmentCollectionDTO achDTO = new AchievmentCollectionDTO();

            List<AchievmentComplex> achList = await accAchRepository.GetUserAchievments(appId, accId);

            foreach (var child in achList)
            {
                achDTO.Entries.Add(new AchievmentDTO { Id = child.Id, Name = child.Name, Description = child.Description, ImageData = child.ImageData, ImageMimeType = child.ImageMimeType, Reached = child.Reached });
            }

            return achDTO;
        }

        public async Task<bool> SetAchievmentReached(int achId, int accId)
        {
            AccAch ach = new AccAch { AccountId = accId, AchievmentId = achId, Reached = true };

            if (await accAchRepository.UpdateAsync(ach))
            {
                if (await unitOfWork.SaveAsync())
                    return true;
            }

            return false;
        }

        public async Task<bool> SetAchievmentUnreached(int achId, int accId)
        {
            AccAch ach = new AccAch { AccountId = accId, AchievmentId = achId, Reached = false };

            if (await accAchRepository.UpdateAsync(ach))
            {
                if (await unitOfWork.SaveAsync())
                    return true;
            }

            return false;
        }

        private async Task<bool> ValidateAccess(int accId)
        {
            return false;
        }
    }
}
