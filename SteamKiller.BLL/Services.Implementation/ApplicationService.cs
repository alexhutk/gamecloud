using Microsoft.EntityFrameworkCore;
using SteamKiller.BLL.Entities;
using SteamKiller.DAL.Entites;
using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Interfaces;
using SteamKiller.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteamKiller.DAL.Entities.Complex;

namespace SteamKiller.BLL.Services
{
    public class ApplicationService : IApplicationService
    {
        private IUnitOfWork unitOfWork;
        private IApplicationRepository appRepository;
        private IAppAccRepository accRepository;

        public ApplicationService(IUnitOfWork _unit, IApplicationRepository apR, IAppAccRepository acR)
        {
            unitOfWork = _unit;
            appRepository = apR;
            accRepository = acR;
        }

        public async Task<int> AddApplication(ApplicationDTO _app, int accId)
        {
            Application app = new Application();
            app.Name = _app.Name;

            if (_app.ImageData != null)
            {
                app.ImageMimeType = _app.ImageMimeType;
                app.ImageData = new byte[_app.ImageData.Length];
                app.ImageData = _app.ImageData;
            }

            return await unitOfWork.CreateApplication(app, accId);
        }

        public async Task<bool> AddApplicationToAccount(int appId, int accId)
        {
            if (await appRepository.ContainsAsync(appId))
            {
                if (await accRepository.CheckIfAccountHaveApplication(appId, accId) == false)
                {
                    if (await accRepository.AddAsync(new AppAcc { AccountId = accId, ApplicationId = appId }))
                    {
                        return await unitOfWork.SaveAsync();
                    }

                    return false;
                }
            }

            return false;
        }

        public async Task<ApplicationCollectionDTO> GetAllApplications()
        {
            ApplicationCollectionDTO app = new ApplicationCollectionDTO();

            IEnumerable<Application> apps = await appRepository.GetAllAsync();

            foreach (var child in apps)
            {
                app.Applications.Add(new ApplicationDTO
                {
                    Id = child.Id,
                    Name = child.Name,
                    ImageData = child.ImageData,
                    ImageMimeType = child.ImageMimeType
                });
            }

            return app;
        }

        public async Task<ApplicationDTO> GetApplication(int id)
        {
            Application app = await appRepository.FindByIdAsync(id);

            if (app != null)
            {
                ApplicationDTO appDTO = new ApplicationDTO();

                appDTO.Id = app.Id;
                appDTO.Name = app.Name;
                appDTO.ImageData = app.ImageData;
                appDTO.ImageMimeType = app.ImageMimeType;

                return appDTO;
            }
            return null;
        }

        public async Task<ApplicationCollectionDTO> GetUserApplications(int accId)
        {
            ApplicationCollectionDTO appDTO = new ApplicationCollectionDTO();

            IEnumerable<ApplicationComplex> apps = await accRepository.GetUserApplications(accId);

            foreach (var child in apps)
            {
                appDTO.Applications.Add(new ApplicationDTO
                {
                    Id = child.Id,
                    Name = child.Name,
                    ImageData = child.ImageData,
                    ImageMimeType = child.ImageMimeType,
                    IsAdmin = child.IsAdmin
                });
            }

            return appDTO;
        }

        public async Task<bool> RemoveApplication(int id)
        {
            bool success = await appRepository.DeleteAsync(id);

            if (success)
            {
                return await unitOfWork.SaveAsync();
            }

            return false;
        }

        public async Task<bool> RemoveApplicationFromAccount(int appId, int accId)
        {
            AppAcc appToDelete = await accRepository.GetUserApplication(appId, accId);

            if (appToDelete != null)
            {
                if (await accRepository.DeleteAsync(e=>e.Id == appToDelete.Id))
                {
                    return await unitOfWork.SaveAsync();
                }

                return false;
            }

            return false;
        }

        public async Task<bool> UpdateApplication(ApplicationDTO appDTO)
        {
            Application app = new Application { Id = appDTO.Id, Name = appDTO.Name, ImageData = appDTO.ImageData, ImageMimeType = appDTO.ImageMimeType };

            if (await appRepository.UpdateAsync(app))
            {
                return await unitOfWork.SaveAsync();
            }

            return false;
        }
    }
}
