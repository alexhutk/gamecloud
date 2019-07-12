using Microsoft.AspNetCore.Http;
using SteamKiller.BLL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public IFormFile AvatarFile { get; set; }
        public int ApplicationID { get; set; }
        public Permission Permission { get; set; }
        public bool PermissionValue { get; set; }
    }
}
