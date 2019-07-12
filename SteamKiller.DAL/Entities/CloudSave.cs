using SteamKiller.DAL.Entities.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Entities
{
    public class CloudSave
    {
        public int Id { get; set; }
        public byte[] SaveData { get; set; }
        public DateTime SaveTime { get; set;}

        public List<AppAccSave> AppAccSaves { get; set; }

        public CloudSave()
        {
            AppAccSaves = new List<AppAccSave>();
        }
    }
}
