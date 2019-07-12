using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SteamKiller.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("docs")]
    public class DocumentationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}