using Microsoft.AspNetCore.Mvc;
using SteamKiller.BLL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Models
{
    public class PermissionViewModel
    {
        public int AccId { get; set; }
        public int AppId { get; set; }
        public string Name { get; set; }
        [FromQuery]
        public Permission Perm { get; set; }
        [FromQuery]
        public bool Value { get; set; }
    }
}
