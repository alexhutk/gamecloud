using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SteamKiller.DPL.Models;
using SteamKiller.DPL.Abstract;
using SteamKiller.BLL.Interfaces;
using SteamKiller.BLL.Entities;
using SteamKiller.WEB.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using SteamKiller.BLL.Entities.Enums;
using SteamKiller.BLL.Infrastructure.Extensions;
using SteamKiller.WEB.Models.Leaderboard;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SteamKiller.DPL.Controllers
{
    [Route("api/v1/applications/{appId}/leaderboards")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class LeaderboardController : Controller
    {
        private ILeaderboardService leaderService;
        private IAccountService accService;

        public LeaderboardController(ILeaderboardService _lead, IAccountService _acc)
        {
            leaderService = _lead;
            accService = _acc;
        }

        [HttpPut]
        [Route("{leaderId}")]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        public async Task<IActionResult> AddScore([FromRoute]LeaderboardEntryViewModel vModel)
        {
            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = vModel.AppId }))
                return new UnauthorizedResult();

            long result = await leaderService.AddScoreToLeaderboard(new AccLeaderDTO { LeaderboardId = vModel.LeaderId, AccountId = User.GetAccountId(), Score = vModel.Score });

            if (result > 0)
            {
                LeaderboardEntryReturnViewModel oModel = new LeaderboardEntryReturnViewModel();
                oModel.Status = new OkStatus("Score is added to Leaderboard!");
                oModel.Id = result;
                oModel.LeaderId = vModel.LeaderId;
                oModel.AppId = vModel.AppId;
                oModel.AccId = User.GetAccountId();
                oModel.Score = vModel.Score;

                return Json(oModel);
            }
            else
            {
                return Json(new FailedStatus("Score is NOT added to Leaderboard! Error code: " + result));
            }
        }

        [HttpPut]
        public async Task<IActionResult> AddLeadboard(int appId, string name)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = appId }, permList))
                return new UnauthorizedResult();

            int id = await leaderService.AddLeaderboard(appId, name);

            if (id != -1)
            {
                LeaderboardEntityViewModel vModel = new LeaderboardEntityViewModel();
                vModel.Status = new OkStatus("Leaderboard was created!");
                vModel.Id = id;
                vModel.Name = name;
 
                return Json(vModel);
            }
            else
            {
                return Json(new FailedStatus("Leaderboard wasn't created, because application doesn't exist!"));
            }
        }

        [HttpDelete]
        [Route("{lId}")]
        public async Task<IActionResult> DeleteLeaderboard(int lId, int appId)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = appId }, permList))
                return new UnauthorizedResult();

            bool success = await leaderService.DeleteLeaderboard(lId);

            if (success)
            {
                return Json(new OkStatus("Leaderboard was deleted!"));
            }
            else
            {
                return Json(new FailedStatus("Leaderboard wasn't deleted, because it doesn't exist!"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetApplicationLeaderboard(int appId)
        {
            LeaderboardEntityViewModel vModel = new LeaderboardEntityViewModel();
            LeaderboardSignatureDTO leader = await leaderService.GetLeaderboardsByAppId(appId);

            if (leader != null)
            {
                vModel.Id = leader.Id;
                vModel.Name = leader.Name;
                vModel.Status = new OkStatus("Get Leaderboard: " + vModel.Name);
                return Json(vModel);
            }
            else
            {
                Status status = new FailedStatus("Can't get that leaderboard, because that appId doesn't have leaderboard!");
                return Json(status);
            }
        }
    }
}