using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SteamKiller.BLL.Entities
{
    public class AuthorizedAccountDTO
    {
        public ClaimsIdentity Identity { get; set; }
        public string Avatar { get; set; }
    }
}
