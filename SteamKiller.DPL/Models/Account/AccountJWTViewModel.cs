using SteamKiller.DPL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.DPL.Models
{
    public class AccountJWTViewModel
    {
        public Status Status { get; set; }
        public string UserName { get; set; }
        public string JWTToken { get; set; }
    }
}
