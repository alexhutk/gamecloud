using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Models
{
    public class SaveGameBaseViewModel
    {
        public int Id { get; set; }
        public byte[] SaveData { get; set; }
        public DateTime SaveTime { get; set; }
    }
}
