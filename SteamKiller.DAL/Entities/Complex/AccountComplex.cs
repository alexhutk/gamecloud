using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Entities.Complex
{
    public class AccountComplex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int ApplicationID { get; set; }
        public bool IsAdmin { get; set; }
    }
}
