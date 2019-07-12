using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Models
{
    public class SaveGameEntryViewModel : SaveGameBaseViewModel
    { 
        public int AccountId { get; set; }
        public int ApplicationId { get; set; }
    }
}
