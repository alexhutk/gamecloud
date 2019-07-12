using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Models.Leaderboard
{
    public class LeaderboardEntryViewModel
    {
        public long Id { get; set; }
        public int LeaderId { get; set; }
        public int AccId { get; set; }
        public int AppId { get; set; }
        [FromQuery]
        public int Score { get; set; }
    }
}
