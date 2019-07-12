using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Entities.Enums;
using SteamKiller.BLL.Infrastructure.Extensions;
using SteamKiller.BLL.Services.Interfaces;
using SteamKiller.WEB.Areas.Admin.Models.Report;

namespace SteamKiller.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("report")]
    [Authorize]
    public class ReportController : Controller
    {
        private IReportService reportService;

        public ReportController(IReportService report)
        {
            reportService = report;
        }

        [HttpGet]
        public IActionResult Report()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Report(ReportAddViewModel vModel)
        {
            ReportDTO report = new ReportDTO();
            report.ReporterId = User.GetAccountId();
            report.ReporterName = User.Identity.Name;
            report.Message = vModel.Message;
            report.Type = ReportType.Question;

            await reportService.Send(report);

            return RedirectToAction("Applications", "Application", new { accId = User.GetAccountId(), area = "Admin" });
        }
    }
}