using SteamKiller.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Entities.Links
{
    public class AppAch
    {
        public long Id { get; set; }
        
        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public int AchievmentId { get; set; }
        public Achievment Achievment { get; set; }
    }
}
