using SteamKiller.BLL.Entities;
using SteamKiller.DPL.Abstract;
using SteamKiller.WEB.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.DPL.Models
{
    public class ApplicationViewModel:ApplicationBaseViewModel
    {
        public LeaderboardSignatureDTO Leaderboard { get; set; }
    }
}
