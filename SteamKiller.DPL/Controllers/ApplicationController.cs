using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Entities.Enums;
using SteamKiller.BLL.Infrastructure.Extensions;
using SteamKiller.BLL.Interfaces;
using SteamKiller.DPL.Abstract;
using SteamKiller.DPL.Models;
using SteamKiller.WEB.Infrastructure.Filters;
using SteamKiller.WEB.Models.Application;

namespace SteamKiller.DPL.Controllers
{
    [Route("api/v1/applications")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ApplicationController : Controller
    {
        private IApplicationService appService;
        private IAccountService accService;

        public ApplicationController(IApplicationService _app, IAccountService _acc)
        {
            appService = _app;
            accService = _acc;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplications()
        {
            ApplicationCollectionDTO apps = await appService.GetAllApplications();

            if (apps.Applications.Count() > 0)
            {
                ApplicationListViewModel vModel = new ApplicationListViewModel();
                vModel.Status = new OkStatus("");
                List<ApplicationEntryViewModel> viewList = new List<ApplicationEntryViewModel>();

                foreach (var e in apps.Applications)
                {
                    viewList.Add(new ApplicationEntryViewModel { Id = e.Id, Name = e.Name });
                }

                vModel.EntryList = viewList;
                return Json(vModel);
            }
            else
            {
                Status status = new FailedStatus("There's no applications!");
                return Json(status);
            }
        }

        [HttpGet]
        [Route("{appId}")]
        public async Task<IActionResult> GetApplication(int appId)
        {
            ApplicationDTO app = await appService.GetApplication(appId);

            if (app != null)
            {
                ApplicationViewModel vModel = new ApplicationViewModel()
                {
                    Status = new OkStatus(""),
                    Id = app.Id,
                    Name = app.Name,
                    Leaderboard = app.Leaderboard
                };

                return Json(app);
            }
            else
            {
                return Json(new FailedStatus("Can't find application with given Id!"));
            }
        }

        [HttpGet]
        [Route("{appId}/users")]
        public async Task<IActionResult> GetByApplication(int appId)
        {
            AccountCollectionDTO accDTO = await accService.GetByApplication(appId);

            if (accDTO != null)
            {
                AccountCollectionViewModel vModel = new AccountCollectionViewModel();
                vModel.Status = new OkStatus("");

                foreach (var child in accDTO.Accounts)
                {
                    vModel.EntryList.Add(new AccountEntryViewModel { Id = child.Id, Name = child.Name });
                }

                return Json(vModel);
            }

            return Json(new FailedStatus("No accounts available!"));
        }

        [HttpPut]
        public async Task<IActionResult> AddApplication(string name)
        {
            ApplicationDTO app = new ApplicationDTO { Name = name };

            int entityId = await appService.AddApplication(app, User.GetAccountId());

            if (entityId > 0)
            {
                ApplicationBaseViewModel oModel = new ApplicationBaseViewModel();

                oModel.Status = new OkStatus("The Application " + name + " with id:" + entityId + " was successfully created!");
                oModel.Id = entityId;
                oModel.Name = name;

                return Json(oModel);
            }

            return Json(new FailedStatus("The Application " + name + " wasn't created!"));
        }

        [HttpPut]
        [Route("{appId}")]
        public async Task<IActionResult> UpdateApplication(int appId, string name)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = appId }, permList))
                return new UnauthorizedResult();

            if (await appService.UpdateApplication(new ApplicationDTO { Id = appId, Name = name }))
            {
                ApplicationBaseViewModel oModel = new ApplicationBaseViewModel();
                oModel.Status = new OkStatus("Application: " + appId + " was successfully updated!");
                oModel.Id = appId;
                oModel.Name = name;

                return Json(oModel);
            }

            return Json(new FailedStatus("Application: " + appId + "wasn't updated!"));
        }

        [HttpDelete]
        [Route("{appId}")]
        public async Task<IActionResult> RemoveApplication(int appId)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = appId }, permList))
                return new UnauthorizedResult();

            if (await appService.RemoveApplication(appId))
                return Json(new OkStatus("The Application with id: " + appId + " was successfully deleted!"));
            else
                return Json(new FailedStatus("The Application " + appId + " wasn't deleted!"));
        }
    }
}