using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Entites.Links
{
    public class AccLeader
    {
        public long Id { get; set; }
        public int Score { get; set; }

        public int LeaderboardId { get; set; }
        public Leaderboard Leaderboard { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
