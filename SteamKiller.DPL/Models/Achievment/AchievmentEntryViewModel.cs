using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Models
{
    public class AchievmentEntryViewModel:AchievmentBaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
