using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace SteamKiller.DPL.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        [Route("Root")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
