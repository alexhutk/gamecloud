using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Entities.Enums;
using SteamKiller.BLL.Infrastructure.Extensions;
using SteamKiller.BLL.Interfaces;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.WEB.Areas.Admin.Models.Achievment;
using SteamKiller.WEB.Models;

namespace SteamKiller.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("users/{accId}/applications/{appId}/achievment")]
    [Authorize]
    public class AchievmentController : Controller
    {
        private IAccountService accService;
        private IAchievmentService achService;

        public AchievmentController(IAccountService _acc, IAchievmentService _ach)
        {
            accService = _acc;
            achService = _ach;
        }

        [HttpGet]
        public async Task<IActionResult> Manage(int appId, string appName)
        {
            AchievmentCollectionDTO achDTO = await achService.GetApplicationAchievments(appId);
            AchievmentCollectionViewModel vModel = new AchievmentCollectionViewModel();
            vModel.AppId = appId;
            vModel.Name = appName;

            if (achDTO.Entries.Count > 0)
            {
                foreach (var c in achDTO.Entries)
                {
                    vModel.Entries.Add(new AchievmentEntryViewModel { Id = c.Id, Name = c.Name, Description = c.Description, ImageData = c.ImageData, ImageMimeType = c.ImageMimeType });
                }
            }

            return View(vModel);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(AchievmentAddViewModel vModel)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = vModel.AppId }, permList))
                return new UnauthorizedResult();

            AchievmentDTO achDTO = new AchievmentDTO();
            achDTO.ApplicationId = vModel.AppId;
            achDTO.Name = vModel.Name;
            achDTO.Description = vModel.Description;

            if (vModel.Image != null)
            {
                achDTO.ImageMimeType = vModel.Image.ContentType;

                using (var binaryReader = new BinaryReader(vModel.Image.OpenReadStream()))
                {
                    achDTO.ImageData = binaryReader.ReadBytes((int)vModel.Image.Length);
                }
            }

            await achService.AddAchievment(achDTO);

            return RedirectToAction("Manage", "Achievment", new { area = "Admin", appId = vModel.AppId, appName = vModel.AppName });
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteAchievment(int id, int appId, string appName)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = appId }, permList))
                return new UnauthorizedResult();

            AchievmentDTO achDTO = new AchievmentDTO { Id = id, AccountId = User.GetAccountId() };

            await achService.RemoveAchievment(achDTO);

            return RedirectToAction("Manage", "Achievment", new { area = "Admin", appId = appId, appName = appName });
        }
    }
}