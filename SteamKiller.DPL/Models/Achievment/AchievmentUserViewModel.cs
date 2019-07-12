using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Models
{
    public class AchievmentUserViewModel:AchievmentBaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Reached { get; set; }
        public int ApplicationId { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
