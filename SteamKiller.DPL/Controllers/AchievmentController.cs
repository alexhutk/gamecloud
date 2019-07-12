using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.DAL.EntitiesFramefork;
using SteamKiller.DPL.Models;
using SteamKiller.WEB.Infrastructure.Filters;
using SteamKiller.WEB.Models;
using SteamKiller.BLL.Infrastructure.Extensions;
using SteamKiller.BLL.Interfaces;
using SteamKiller.BLL.Entities.Enums;
using SteamKiller.WEB.Models.Achievment;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SteamKiller.WEB.Controllers
{
    [Route("api/v1/applications/{appId}/achievments")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AchievmentController : Controller
    {
        private IAchievmentService achService;
        private IAccountService accService;

        public AchievmentController(IAchievmentService _ach, IAccountService _acc)
        {
            achService = _ach;
            accService = _acc;
        }

        [HttpGet]
        [Route("{achId}")]
        public async Task<IActionResult> GetAchievmentSummary(int appId, int achId)
        {
            AchievmentCollectionDTO achDTO = await achService.GetAchievmentSummaryByApplication(appId, achId);

            if (achDTO.Entries.Count > 0)
            {
                AchievmentCollectionViewModel vModel = new AchievmentCollectionViewModel();
                vModel.Status = new OkStatus("");

                foreach (var c in achDTO.Entries)
                {
                    vModel.Entries.Add(new AchievmentSummaryViewModel { Id = c.Id, Reached = c.Reached, AccountId = c.Id, ApplicationId = c.ApplicationId});
                }

                return Json(vModel);
            }

            return Json(new FailedStatus("Nothing found!"));
        }

        [HttpGet]
        public async Task<IActionResult> GetApplicationAchievments(int appId)
        {
            AchievmentCollectionDTO achDTO = await achService.GetApplicationAchievments(appId);

            if (achDTO.Entries.Count > 0)
            {
                AchievmentCollectionViewModel vModel = new AchievmentCollectionViewModel();
                vModel.Status = new OkStatus("");

                foreach (var c in achDTO.Entries)
                {
                    vModel.Entries.Add(new AchievmentEntryViewModel { Id = c.Id, Name = c.Name, Description = c.Description, ImageData = c.ImageData, ImageMimeType = c.ImageMimeType });
                }

                return Json(vModel);
            }

            return Json(new FailedStatus("Nothing found!"));
        }

        [HttpPut]
        public async Task<IActionResult> AddNewAchievment(int appId, string name, string description)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = appId }, permList))    
                return new UnauthorizedResult();

            AchievmentDTO achDTO = new AchievmentDTO { ApplicationId = appId, Name = name, Description = description, ImageData = Encoding.ASCII.GetBytes("24"), ImageMimeType = "png" };

            int result = await achService.AddAchievment(achDTO);

            if (result < 0)
            {
                return Json(new FailedStatus("Error while adding new achievment! ErrorCode: " + result));
            }

            AchievmentInfoViewModel oModel = new AchievmentInfoViewModel();
            oModel.Status = new OkStatus("Achievment was successfully added! Ach id: " + result);
            oModel.Id = result;
            oModel.Name = name;
            oModel.Description = description;
            oModel.ImageData = Encoding.ASCII.GetBytes("24");
            oModel.ImageMimeType = "png";

            return Json(oModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAchievment(int id, int appId, string name, string description)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = appId }, permList))
                return new UnauthorizedResult();

            AchievmentDTO achDTO = new AchievmentDTO { Id = id, Name = name, Description = description, ImageData = Encoding.ASCII.GetBytes("24"), ImageMimeType = "png" };

            if (await achService.UpdateAchievment(achDTO))
            {
                AchievmentInfoViewModel oModel = new AchievmentInfoViewModel();
                oModel.Status = new OkStatus("Achievment with Id: " + id + " was successfully updated!");
                oModel.Id = id;
                oModel.Name = name;
                oModel.Description = description;
                oModel.ImageData = Encoding.ASCII.GetBytes("24");
                oModel.ImageMimeType = "png";

                return Json(oModel);
            }

            return Json(new FailedStatus("Fail to update achievment!"));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAchievment(int id, int appId)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = appId }, permList))
                return new UnauthorizedResult();

            AchievmentDTO achDTO = new AchievmentDTO { Id = id, AccountId = User.GetAccountId() };

            if (await achService.RemoveAchievment(achDTO))
            {
                return Json(new OkStatus("Achievment with Id: " + id + " was successfully removed!"));
            }

            return Json(new FailedStatus("Fail to remove achievment!"));
        }

        [HttpPut]
        [Route("~/api/v1/users/{accId}/applications/{appId}/achievments/{achId}")]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        public async Task<IActionResult> SetAchievment(int appId, int achId, int accId, bool reached)
        {
            if (reached)
            {
                if (await achService.SetAchievmentReached(achId, accId))
                {
                    return Json(new OkStatus("Achievment " + achId + " was set up reached!"));
                }
            }
            else
            {
                if (await achService.SetAchievmentUnreached(achId, accId))
                {
                    return Json(new OkStatus("Achievment " + achId + " was set up unreached!"));
                }
            }

            return Json(new FailedStatus("Can't set this achievment!"));
        }
    }
}