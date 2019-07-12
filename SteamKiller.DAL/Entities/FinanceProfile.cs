using SteamKiller.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Entities
{
    public class FinanceProfile
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string BankName { get; set; }
        public string IbanNumber { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
