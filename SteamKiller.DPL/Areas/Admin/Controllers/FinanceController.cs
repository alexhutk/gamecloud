using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Infrastructure.Extensions;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.WEB.Areas.Admin.Models.Finance;
using SteamKiller.WEB.Infrastructure.Filters;

namespace SteamKiller.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("users/{accId}/finance")]
    [Authorize]
    public class FinanceController : Controller
    {
        private IFinanceService finService;

        public FinanceController(IFinanceService _fin)
        {
            finService = _fin;
        }

        [HttpGet]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        public async Task<IActionResult> FinanceProfile(int accId)
        {
            FinanceProfileDTO finDTO = await finService.GetAccountFinanceProfile(accId);
            FinanceProfileViewModel vModel = new FinanceProfileViewModel();

            if (finDTO != null)
            {
                vModel.Address = finDTO.Address;
                vModel.BankName = finDTO.BankName;
                vModel.IbanNumber = finDTO.IbanNumber;
            }

            return View(vModel);
        }

        [HttpPost]
        [ServiceFilter(typeof(AccountIdValidatorFilter))]
        public async Task<IActionResult> FinanceProfile(FinanceProfileViewModel vModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data!");
                return View(vModel);
            }

            FinanceProfileDTO finDTO = new FinanceProfileDTO()
            {
                Address = vModel.Address,
                BankName = vModel.BankName,
                IbanNumber = vModel.IbanNumber,
                AccountId = User.GetAccountId()
            };

            int id = await finService.AddFinanceProfile(finDTO);

            if (id < 0)
            {
                ModelState.AddModelError("", "Error while adding data to database! Error code: " + id);
                return View(vModel);
            }

            return RedirectToAction("Applications", "Application", new { accId = User.GetAccountId(), area = "Admin" });
        }
    }
}