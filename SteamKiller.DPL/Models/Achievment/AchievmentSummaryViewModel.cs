using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Models
{
    public class AchievmentSummaryViewModel:AchievmentBaseViewModel
    {
        public bool Reached { get; set; }
        public int ApplicationId { get; set; }
        public int AccountId { get; set; }
    }
}
