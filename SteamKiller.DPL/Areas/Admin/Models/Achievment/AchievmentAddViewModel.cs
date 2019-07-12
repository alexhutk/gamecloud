using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Areas.Admin.Models.Achievment
{
    public class AchievmentAddViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int AppId { get; set; }
        public string AppName { get; set; }
        public IFormFile Image { get; set; }
    }
}
