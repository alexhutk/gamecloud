using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.DPL.Models
{
    public class ApplicationEntryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public bool IsAdmin { get; set; }
    }
}
