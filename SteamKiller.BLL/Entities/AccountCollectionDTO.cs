using SteamKiller.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities
{
    public class AccountCollectionDTO
    {
        public List<AccountDTO> Accounts { get; set; }

        public AccountCollectionDTO()
        {
            Accounts = new List<AccountDTO>();
        }
    }
}
