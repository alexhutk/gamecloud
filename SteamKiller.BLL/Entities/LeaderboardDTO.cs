using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities
{
    public class LeaderboardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LeaderboardEntryDTO> EntryList { get; set; }

        public LeaderboardDTO()
        {
            EntryList = new List<LeaderboardEntryDTO>();
        }
    }
}
