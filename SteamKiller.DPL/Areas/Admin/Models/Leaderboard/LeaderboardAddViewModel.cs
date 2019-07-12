using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Areas.Admin.Models.Leaderboard
{
    public class LeaderboardAddViewModel
    {
        public int Id { get; set; }
        [Required]
        public int AppId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
