﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SteamKiller.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("users/{accId}/applications/{appId}/analytics")]
    [Authorize]
    public class AnalyticsController : Controller
    {
        public IActionResult AnalyticsMainPaige()
        {
            return View("NotImplemented");
        }
    }
}