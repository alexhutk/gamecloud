using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Entities.Enums;
using SteamKiller.BLL.Infrastructure.Extensions;
using SteamKiller.BLL.Interfaces;
using SteamKiller.DPL.Abstract;
using SteamKiller.DPL.Models;
using SteamKiller.WEB.Areas.Admin.Models.Leaderboard;

namespace SteamKiller.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("users/{accId}/applications/{appId}/leaderboard")]
    [Authorize]
    public class LeaderboardController : Controller
    {
        private ILeaderboardService leaderService;
        private IAccountService accService;

        public LeaderboardController(ILeaderboardService _leader, IAccountService _acc)
        {
            leaderService = _leader;
            accService = _acc;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddLeadboard(LeaderboardAddViewModel vModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Model error!");
                LeaderboardAddViewModel oModel = new LeaderboardAddViewModel { Id = vModel.Id, AppId = vModel.AppId, Name = vModel.Name };
                return View("GetApplicationLeaderboard", oModel);
            }

            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = vModel.AppId }, permList))
                return new UnauthorizedResult();

            int id = await leaderService.AddLeaderboard(vModel.AppId, vModel.Name);

            if (id != -1)
            {
                return RedirectToAction("ManageApplication", "Application", new { accId = User.GetAccountId(), appId = vModel.AppId, area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("", "Error while creating leaderboard!");
                LeaderboardAddViewModel oModel = new LeaderboardAddViewModel { Id = vModel.Id, AppId = vModel.AppId, Name = vModel.Name };
                return View("GetApplicationLeaderboard", oModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLeaderboard(LeaderboardAddViewModel vModel)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = vModel.AppId }, permList))
                return new UnauthorizedResult();

            bool success = await leaderService.DeleteLeaderboard(vModel.Id);

            if (success)
            {
                return RedirectToAction("ManageApplication", "Application", new { accId = User.GetAccountId(), appId = vModel.AppId, area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("", "Can't delete that leaderboard!");
                return View(vModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetApplicationLeaderboard(LeaderboardAddViewModel vModel = null)
        {
            LeaderboardAddViewModel oModel = new LeaderboardAddViewModel();
            oModel.AppId = vModel.AppId;

            LeaderboardSignatureDTO leader = await leaderService.GetLeaderboardsByAppId(vModel.AppId);

            if (leader != null)
            {
                vModel.Id = leader.Id;
                vModel.Name = leader.Name;
                return View(vModel);
            }

            return View(oModel);
        }
    }
}