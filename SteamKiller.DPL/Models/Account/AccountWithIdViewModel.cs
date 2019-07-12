﻿using SteamKiller.DPL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.DPL.Models
{
    public class AccountWithIdViewModel
    {
        public Status Status { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
