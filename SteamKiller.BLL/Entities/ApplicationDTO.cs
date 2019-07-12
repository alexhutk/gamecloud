using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities
{
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LeaderboardSignatureDTO Leaderboard { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public bool IsAdmin { get; set; }
    }
}
