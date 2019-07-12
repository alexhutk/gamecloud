using Microsoft.AspNetCore.Mvc;
using SteamKiller.DPL.Models;
using System.Threading.Tasks;
using System;
using System.IdentityModel.Tokens.Jwt;
using SteamKiller.DPL.Identity.JWT;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using SteamKiller.BLL.Interfaces;
using SteamKiller.BLL.Entities;
using Microsoft.AspNetCore.Authorization;
using SteamKiller.WEB.Infrastructure.Filters;
using SteamKiller.BLL.Entities.Enums;
using SteamKiller.BLL.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;
using SteamKiller.DPL.Abstract;
using SteamKiller.WEB.Models;
using SteamKiller.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SteamKiller.DPL.Controllers
{
    [Route("api/v1/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AccountController : Controller
    {
        private IAccountService accService;
        private IApplicationService appService;
        private IAchievmentService achService;
        private ISecurityService secService;

        public AccountController(IAccountService _acc, IApplicationService _app, IAchievmentService _ach, ISecurityService _sec)
        {
            accService = _acc;
            appService = _app;
            achService = _ach;
            secService = _sec;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            AccountCollectionDTO accDTO = await accService.GetAll();

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

        [HttpGet]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        [Route("{accId}/applications")]
        public async Task<IActionResult> UserApplications(int accId)
        {
            ApplicationCollectionDTO apps = await appService.GetUserApplications(accId);

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
        [Route("{id}")]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        public async Task<IActionResult> GetById(int id)
        {
            AccountDTO acc = await accService.GetById(id);

            if (acc != null)
            {
                AccountViewModel vModel = new AccountViewModel();
                vModel.Status = new OkStatus("");
                vModel.Name = acc.Name;
                vModel.Password = acc.Password;

                return Json(vModel);
            }

            return Json(new FailedStatus("Can't find account with that parameters!"));
        }

        [HttpGet]
        [Route("{name}")]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        public async Task<IActionResult> GetByName(string name)
        {
            AccountDTO acc = await accService.GetByName(name);

            if (acc != null)
            {
                AccountWithIdViewModel vModel = new AccountWithIdViewModel();
                vModel.Status = new OkStatus("");
                vModel.Id = acc.Id;
                vModel.Name = acc.Name;
                vModel.Password = acc.Password;

                return Json(vModel);
            }

            return Json(new FailedStatus("Can't find account with that parameters!"));
        }

        [HttpGet]
        [Route("{accId}/applications/{appId}/saves")]
        public async Task<IActionResult> GetUserAchievments(int appId, int accId)
        {
            AchievmentCollectionDTO achDTO = await achService.GetUserAchievments(appId, accId);

            if (achDTO.Entries.Count > 0)
            {
                AchievmentCollectionViewModel vModel = new AchievmentCollectionViewModel();
                vModel.Status = new OkStatus("");

                foreach (var c in achDTO.Entries)
                {
                    vModel.Entries.Add(new AchievmentUserViewModel { Id = c.Id, Name = c.Name, Reached = c.Reached, ApplicationId = c.ApplicationId, Description = c.Description, ImageData = c.ImageData, ImageMimeType = c.ImageMimeType });
                }

                return Json(vModel);
            }

            return Json(new FailedStatus("Nothing found!"));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddNewAccount([FromQuery]AccountAddViewModel vModel)
        {
            if(!ModelState.IsValid)
                return Json(new FailedStatus("Given data is not correct!"));

            if (vModel.Password != vModel.ConfirmPassword)
                return Json(new FailedStatus("Passwords don't match!"));

            int accountId = await accService.AddAccount(new AccountDTO { Name = vModel.Name, Password = vModel.Password });

            if (accountId > 0)
            {
                AccountWithIdViewModel oModel = new AccountWithIdViewModel();
                oModel.Status = new OkStatus("Account with id: " + accountId + " was created!");
                oModel.Id = accountId;
                oModel.Name = vModel.Name;
                oModel.Password = vModel.Password;

                return Json(oModel);
            }

            return Json(new FailedStatus("Can't create account with given parameters!"));
        }

        [HttpPut]
        [Route("{accId}/applications/{appId}")]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        public async Task<IActionResult> AddApplicationToUser(int accId, int appId)
        {
            if (await appService.AddApplicationToAccount(appId, accId))
                return Json(new OkStatus("The Application " + appId + " was successfully added to user " + accId));
            else
                return Json(new FailedStatus("The Application " + appId + " wasn't added to user " + accId));
        }

        [HttpPut]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        public async Task<IActionResult> UpdateAccount([FromQuery]AccountUpdateViewModel vModel)
        {
            if (!ModelState.IsValid)
                return Json(new FailedStatus("Given data is not correct!"));

            if (await accService.UpdateAccount(new AccountDTO { Id = vModel.Id, Name = vModel.Name, Password = vModel.Password }))
            {
                AccountWithIdViewModel oModel = new AccountWithIdViewModel();
                oModel.Status = new OkStatus("Account with id: " + vModel.Id + " was updated!");
                oModel.Id = vModel.Id;
                oModel.Name = vModel.Name;
                oModel.Password = vModel.Password;

                return Json(oModel);
            }

            return Json(new FailedStatus("Can't create account with given parameters!"));
        }

        [HttpDelete]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (!ModelState.IsValid)
                return Json(new FailedStatus("Given data is not correct!"));

            if (await accService.RemoveAccount(id))
            {
                return Json(new OkStatus("Account with id: " + id + " was deleted!"));
            }

            return Json(new FailedStatus("Can't delete account with given parameters!"));
        }

        [HttpDelete]
        [Route("{accId}/applications/{appId}")]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        public async Task<IActionResult> RemoveApplicationFromUser(int accId, int appId)
        {
            if (await appService.RemoveApplicationFromAccount(appId, accId))
                return Json(new OkStatus("The Application " + appId + " was successfully removed from user " + accId));
            else
                return Json(new FailedStatus("The Application " + appId + " wasn't removed from user " + accId));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromQuery]AccountLoginViewModel acc)
        {
            var authAcc = await accService.Login(new AccountDTO { Name = acc.Name, Password = acc.Password }, "Token");

            if (authAcc == null)
            {
                return Json(new FailedStatus("Incorrent username/password!"));
            }

            var encodedJwt = secService.GenerateEncodedJwt(authAcc.Identity);

            AccountJWTViewModel vModel = new AccountJWTViewModel();
            vModel.Status = new OkStatus("");
            vModel.UserName = authAcc.Identity.Name;
            vModel.JWTToken = encodedJwt;

            return Json(vModel);           
        }

        [HttpPut]
        [Route("{AccId}/applications/{AppId}/permissions")]
        public async Task<IActionResult> SetUserPermission([FromRoute]PermissionViewModel vModel)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = vModel.AppId }, permList))
                return new UnauthorizedResult();

            AccountDTO accDTO = new AccountDTO();
            accDTO.Id = vModel.AccId;
            accDTO.ApplicationID = vModel.AppId;
            accDTO.Permission = vModel.Perm;
            accDTO.PermissionValue = vModel.Value;

            if (await accService.SetUserPermission(accDTO))
                return Json(new OkStatus("Permission: " + vModel.Perm + " was granted for user: " + vModel.AccId + " with value: " + vModel.Value));

            return Json(new FailedStatus("Permission wasn't granted for that user"));
        }
    }
}
