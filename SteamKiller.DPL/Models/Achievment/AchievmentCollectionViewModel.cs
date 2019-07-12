using SteamKiller.DPL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Models
{
    public class AchievmentCollectionViewModel
    {
        public Status Status { get; set; }
        public int AppId { get; set; }
        public string Name { get; set; }
        public List<AchievmentBaseViewModel> Entries { get; set; }

        public AchievmentCollectionViewModel()
        {
            Entries = new List<AchievmentBaseViewModel>();
        }
    }
}
