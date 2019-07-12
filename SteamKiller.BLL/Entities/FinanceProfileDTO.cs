using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities
{
    public class FinanceProfileDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string BankName { get; set; }
        public string IbanNumber { get; set; }
        public int AccountId { get; set; }
    }
}
