using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Infrastructure.Extensions;
using SteamKiller.BLL.Interfaces;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.DPL.Models;
using SteamKiller.WEB.Infrastructure.Filters;
using SteamKiller.WEB.Models;

namespace SteamKiller.WEB.Controllers
{
    [Route("api/v1/applications/{appId}/saves")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CloudSaveController : Controller
    {
        private ICloudSaveService saveService;
        private IAccountService accService;

        public CloudSaveController(ICloudSaveService _save, IAccountService _acc)
        {
            saveService = _save;
            accService = _acc;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSaves(int appId)
        {
            SaveGameCollectionDTO saveDTO = await saveService.GetUserSaveGames(User.GetAccountId(), appId);

            if (saveDTO.EntryList.Count > 0)
            {
                SaveGameCollectionViewModel vModel = new SaveGameCollectionViewModel();
                vModel.Status = new OkStatus("");

                foreach (var c in saveDTO.EntryList)
                {
                    vModel.EntryList.Add(new SaveGameBaseViewModel
                    {
                        Id = c.Id,
                        SaveData = c.SaveData,
                        SaveTime = c.SaveTime
                    });
                }

                return Json(vModel);
            }

            return Json(new FailedStatus("Can't find savegames for given user!"));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSaveGame(int id, int appId)
        {
            if(!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = appId }))
                return new UnauthorizedResult();

            if (await saveService.RemoveSaveGame(id))
                return Json(new OkStatus("Savegame was successfully deleted!"));

            return Json(new FailedStatus("Can't delete savegame with given id!"));
        }

        [HttpPut]
        [Route("{id?}")]
        public async Task<IActionResult> SaveGame(int appId, DateTime time, int id = 0)
        {
            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = appId }))
                return new UnauthorizedResult();

            SaveGameDTO saveDTO = new SaveGameDTO
            {
                Id = id,
                SaveData = Encoding.ASCII.GetBytes("40"),
                SaveTime = time,
                AccountId = User.GetAccountId(),
                ApplicationId = appId
            };

            int resId = await saveService.SaveGame(saveDTO);

            if (resId > 0)
            {
                return Json(new OkStatus("Game was saved! Save id: " + resId));
            }

            return Json(new FailedStatus("Error occurred while saving game! Error code: " + resId));
        }
    }
}