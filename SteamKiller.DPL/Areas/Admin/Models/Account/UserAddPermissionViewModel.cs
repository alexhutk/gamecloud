using Microsoft.AspNetCore.Mvc.Rendering;
using SteamKiller.BLL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Areas.Admin.Models.Account
{
    public class UserAddPermissionViewModel
    {
        public int AppId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Permission Perm { get; set; }
    }
}
