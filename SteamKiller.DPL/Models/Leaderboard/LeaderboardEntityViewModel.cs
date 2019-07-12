using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamKiller.DPL.Abstract;

namespace SteamKiller.DPL.Models
{
    public class LeaderboardEntityViewModel
    {
        public Status Status { get; set; }
        public int Id { get; set; }
        public int AppId { get; set; }
        public string Name { get; set; }
    }
}
