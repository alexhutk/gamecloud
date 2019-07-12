using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Areas.Admin.Models.Application
{
    public class ApplicationAddViewModel
    {
        [Required]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
