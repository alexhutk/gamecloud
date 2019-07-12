using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SteamKiller.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("sdk")]
    public class SdkController : Controller
    {
        [HttpGet]
        [Route("unity")]
        public IActionResult GetUnitySdk()
        {
            return View("NotImplemented");
        }

        [HttpGet]
        [Route("unreal")]
        public IActionResult GetUnrealSdk()
        {
            return View("NotImplemented");
        }
    }
}