using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.DPL.Models
{
    public class AccountAddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public IFormFile Image { get; set; }
        public string Avatar { get; set; }
    }
}
