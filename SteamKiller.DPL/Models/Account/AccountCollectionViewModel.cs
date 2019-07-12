using SteamKiller.DPL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.DPL.Models
{
    public class AccountCollectionViewModel
    {
        public Status Status { get; set; }
        public int AppId { get; set; }
        public List<AccountEntryViewModel> EntryList { get; set; }

        public AccountCollectionViewModel()
        {
            EntryList = new List<AccountEntryViewModel>();
        }
    }
}
