using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamKiller.DAL.Entites;
using SteamKiller.DPL.Abstract;

namespace SteamKiller.DPL.Models
{
    public class ApplicationListViewModel
    {
        public Status Status { get; set; }
        public List<ApplicationEntryViewModel> EntryList { get; set; }

        public ApplicationListViewModel()
        {
            EntryList = new List<ApplicationEntryViewModel>();
        }
    }
}
