using SteamKiller.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Entities.Links
{
    public class AccAch
    {
        public long Id { get; set; }
        public bool Reached { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int AchievmentId { get; set; }
        public Achievment Achievment { get; set; }
    }
}
