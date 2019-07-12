using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Entities.Enums;
using SteamKiller.BLL.Infrastructure.Extensions;
using SteamKiller.BLL.Interfaces;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.DPL.Models;
using SteamKiller.WEB.Areas.Admin.Models.Application;
using SteamKiller.WEB.Infrastructure.Filters;
using SteamKiller.WEB.Models.Application;

namespace SteamKiller.WEB.Areas.Admin.Controllers
{         
    [Area("Admin")]
    [Route("users/{accId}")]
    [Authorize]
    public class ApplicationController : Controller
    {

        private IApplicationService appService;
        private IAccountService accService;
        private ISearchService searchService;

        public ApplicationController(IApplicationService _app, IAccountService _acc, ISearchService _search)
        {
            appService = _app;
            accService = _acc;
            searchService = _search;
        }

        [HttpGet]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        [Route("applications")]
        public async Task<IActionResult> Applications(int accId)
        {
            ApplicationCollectionDTO apps = await appService.GetUserApplications(accId);

            ApplicationListViewModel vModel = new ApplicationListViewModel();
            vModel.Status = new OkStatus("");
            List<ApplicationEntryViewModel> viewList = new List<ApplicationEntryViewModel>();

            foreach (var e in apps.Applications)
            {
                viewList.Add(new ApplicationEntryViewModel { Id = e.Id, Name = e.Name, Image = e.ImageData, IsAdmin = e.IsAdmin });
            }

            vModel.EntryList = viewList;
            return View(vModel);
        }

        [HttpGet]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        [Route("applications/search")]
        public async Task<IActionResult> SearchApplications(int accId, string searchQuery)
        {
            ApplicationCollectionDTO apps = await searchService.SearchApplication(accId, searchQuery);

            ApplicationListViewModel vModel = new ApplicationListViewModel();
            vModel.Status = new OkStatus("");
            List<ApplicationEntryViewModel> viewList = new List<ApplicationEntryViewModel>();

            foreach (var e in apps.Applications)
            {
                viewList.Add(new ApplicationEntryViewModel { Id = e.Id, Name = e.Name, Image = e.ImageData, IsAdmin = e.IsAdmin });
            }

            vModel.EntryList = viewList;

            return View("Applications", vModel);
        }

        [HttpGet]
        [Route("applications/add")]
        public IActionResult AddApplication()
        {
            return View();
        }

        [HttpGet]
        [Route("applications/{appId}")]
        public async Task<IActionResult> ManageApplication(int appId)
        {
            ApplicationDTO appDTO = await appService.GetApplication(appId);
            ApplicationEntryViewModel vModel = new ApplicationEntryViewModel();

            vModel.Id = appDTO.Id;
            vModel.Name = appDTO.Name;
            vModel.Image = appDTO.ImageData;

            return View(vModel);
        }

        [HttpPost]
        [Route("applications/add")]
        public async Task<IActionResult> AddApplication(ApplicationAddViewModel vModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationDTO appDTO = new ApplicationDTO();

                appDTO.Name = vModel.Name;

                if (vModel.Image != null)
                {
                    appDTO.ImageMimeType = vModel.Image.ContentType;

                    using (var binaryReader = new BinaryReader(vModel.Image.OpenReadStream()))
                    {
                        appDTO.ImageData = binaryReader.ReadBytes((int)vModel.Image.Length);
                    }
                }

                await appService.AddApplication(appDTO, User.GetAccountId());
            }
            else
            {
                ModelState.AddModelError("", "Error while creating new app!");
                return View(vModel);
            }

            return RedirectToAction("Applications", "Application", new { accId = User.GetAccountId(), area = "Admin" });
        }

        [HttpPost]
        [Route("applications/{appId}/update")]
        public async Task<IActionResult> UpdateApplication(ApplicationUpdateViewModel vModel)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = vModel.Id}, permList))
                return new UnauthorizedResult();

            ApplicationDTO appDTO = new ApplicationDTO { Id = vModel.Id, Name = vModel.Name };

            if (vModel.Image != null)
            {
                appDTO.ImageMimeType = vModel.Image.ContentType;

                using (var binaryReader = new BinaryReader(vModel.Image.OpenReadStream()))
                {
                    appDTO.ImageData = binaryReader.ReadBytes((int)vModel.Image.Length);
                }
            }

            if (await appService.UpdateApplication(appDTO))
            {
                return RedirectToAction("Applications", "Application", new { accId = User.GetAccountId(), area = "Admin" });
            }

            ModelState.AddModelError("", "Error while updating that application!");
            return View(vModel);
        }

        [HttpPost]
        [Route("applications")]
        public async Task<IActionResult> RemoveApplication(int appId)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = appId }, permList))
                return new UnauthorizedResult();

            await appService.RemoveApplication(appId);
            return RedirectToAction("Applications", "Application", new { accId = User.GetAccountId(), area = "Admin" });
        }
    }
}