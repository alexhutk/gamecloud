using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Entities.Enums;
using SteamKiller.BLL.Infrastructure.Extensions;
using SteamKiller.BLL.Interfaces;
using SteamKiller.DPL.Abstract;
using SteamKiller.DPL.Models;
using SteamKiller.WEB.Areas.Admin.Models.Account;
using SteamKiller.WEB.Infrastructure.Filters;
using SteamKiller.WEB.Models;

namespace SteamKiller.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("users")]
    [Authorize]
    public class AccountController : Controller
    {
        private IAccountService accService;
        private IApplicationService appService;

        public AccountController(IAccountService _acc, IApplicationService _app)
        {
            accService = _acc;
            appService = _app;
        }

        [HttpGet]
        [Route("~/")]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.GetAccountId() > 0)
            {
                return RedirectToAction("Applications", "Application", new { accId = User.GetAccountId(), area = "Admin" });
            }

            return View();
        }

        [HttpGet]
        [Route("{accId}/edit")]
        public async Task<IActionResult> EditAccount(int accId)
        {
            EditAccountViewModel vModel = new EditAccountViewModel();
            AccountDTO account = await accService.GetById(accId);

            if (account != null)
            {
                vModel.Name = account.Name;
                vModel.Avatar = account.Avatar;
            }

            return View(vModel);
        }

        [HttpPost]
        [Route("{accId}/edit")]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        public async Task<IActionResult> EditAccount(EditAccountViewModel vModel)
        {
            if (!ModelState.IsValid)
                return Json(new FailedStatus("Given data is not correct!"));

            AccountDTO accDTO = new AccountDTO { Id = vModel.AccId, Name = vModel.Name };

            if (vModel.Image != null)
            {
                accDTO.AvatarFile = vModel.Image;
            }

            if (await accService.UpdateAccount(accDTO))
            {
                AccountDTO account = await accService.GetById(vModel.AccId);
                SetAvatar(account.Avatar);

                return RedirectToAction("Applications", "Application", new { accId = User.GetAccountId(), area = "Admin" });
            }

            return Json(new FailedStatus("Can't create account with given parameters!"));
        }

        [HttpGet]
        [Route("register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AccountAddViewModel vModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "The given data is not correct!");
                return View(vModel);
            }

            int accountId = await accService.AddAccount(new AccountDTO { Name = vModel.Name, Password = vModel.Password });

            if (accountId > 0)
            {
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }

            return View(vModel);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vModel)
        {
            if (ModelState.IsValid)
            {
                var authAcc = await accService.Login(new AccountDTO { Name = vModel.Username, Password = vModel.Password }, "ApplicationCookie");

                if (authAcc == null)
                {
                    ModelState.AddModelError("", "Can't find user with given credentials!");
                    return View(vModel);
                }

                await Authenticate(authAcc.Identity);

                SetAvatar(authAcc.Avatar);

                return RedirectToAction("Applications", "Application", new { accId = authAcc.Identity.FindFirst(e=>e.Type == "Id").Value, area = "Admin"});
            }

            return View(vModel);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account", new { area = "Admin"});
        }

        [HttpGet]
        [Route("{AccId}/applications/{AppId}/permissions")]
        public IActionResult Permissions()
        {
            UserAddPermissionViewModel vModel = new UserAddPermissionViewModel();
            return View(vModel);
        }

        [HttpGet]
        [Route("{AccId}/applications/{AppId}/users")]
        public async Task<IActionResult> Users(int appId)
        {
            AccountCollectionDTO accDTO = await accService.GetByApplication(appId);
            AccountCollectionViewModel vModel = new AccountCollectionViewModel();

            if (accDTO != null)
            {
                vModel.AppId = appId;

                foreach (var child in accDTO.Accounts)
                {
                    vModel.EntryList.Add(new AccountEntryViewModel { Id = child.Id, Name = child.Name, IsAdmin = child.PermissionValue });
                }
            }

            return View(vModel);
        }

        [HttpPost]
        [Route("{AccId}/applications/{AppId}/permissions")]
        public async Task<IActionResult> Permissions(UserAddPermissionViewModel vModel)
        {
            List<Permission> permList = new List<Permission>
            {
                Permission.Admin
            };

            if (!await accService.CheckPermission(new AccountDTO { Id = User.GetAccountId(), ApplicationID = vModel.AppId }, permList))
                return new UnauthorizedResult();

            AccountDTO accDTO = new AccountDTO();
            accDTO.Name = vModel.Name;
            accDTO.ApplicationID = vModel.AppId;
            accDTO.Permission = vModel.Perm;
            accDTO.PermissionValue = true;

            await accService.SetUserPermissionByName(accDTO);

            return RedirectToAction("Users", "Account", new { area = "Admin", accId = User.GetAccountId(), appId = vModel.AppId });
        }

        private async Task Authenticate(ClaimsIdentity _claims)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(_claims));
        }

        private void SetAvatar(string avatar)
        {
            var options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(10);
            options.IsEssential = true;

            Response.Cookies.Append("Avatar", avatar, options);
        }
    }
}