using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Areas.Admin.Models.Finance
{
    public class FinanceProfileViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Incorrect characters number (must be 10)!")]
        public string IbanNumber { get; set; }
    }
}
