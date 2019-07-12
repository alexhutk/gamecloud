using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities
{
    public class AchievmentCollectionDTO
    {
        public List<AchievmentDTO> Entries { get; set; }

        public AchievmentCollectionDTO()
        {
            Entries = new List<AchievmentDTO>();
        }
    }
}
