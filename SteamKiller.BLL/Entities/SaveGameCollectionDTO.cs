using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities
{
    public class SaveGameCollectionDTO
    {
        public List<SaveGameDTO> EntryList { get; set; }

        public SaveGameCollectionDTO()
        {
            EntryList = new List<SaveGameDTO>();
        }
    }
}
