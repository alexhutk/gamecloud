using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities
{
    public class AccLeaderDTO
    {
        public int LeaderboardId { get; set; }
        public int AccountId { get; set; }
        public int Score { get; set; }
    }
}
