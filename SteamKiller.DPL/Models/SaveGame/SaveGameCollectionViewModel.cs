using SteamKiller.DPL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Models
{
    public class SaveGameCollectionViewModel
    {
        public Status Status { get; set; }
        public List<SaveGameBaseViewModel> EntryList { get; set; }

        public SaveGameCollectionViewModel()
        {
            EntryList = new List<SaveGameBaseViewModel>();
        }
    }
}
