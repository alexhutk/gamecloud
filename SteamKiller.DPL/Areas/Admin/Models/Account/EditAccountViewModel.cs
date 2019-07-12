using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Areas.Admin.Models.Account
{
    public class EditAccountViewModel
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Avatar { get; set; }
        public int AccId { get; set; }
    }
}
