using SteamKiller.DPL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Models.Application
{
    public class ApplicationBaseViewModel
    {
        public Status Status { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
