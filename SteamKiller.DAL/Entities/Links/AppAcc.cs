using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Entites.Links
{
    public class AppAcc
    {
        public long Id { get; set; }
        public bool IsAdmin { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
