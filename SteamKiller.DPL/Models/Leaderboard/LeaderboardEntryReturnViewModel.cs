using SteamKiller.DPL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Models.Leaderboard
{
    public class LeaderboardEntryReturnViewModel:LeaderboardEntryViewModel
    {
        public Status Status { get; set; }
    }
}
