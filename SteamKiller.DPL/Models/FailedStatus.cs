using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamKiller.DPL.Abstract;

namespace SteamKiller.DPL.Models
{
    public class FailedStatus : Status
    {
        public override string State
        {
            get => "Failed!";
            set => base.State = value;
        }

        public FailedStatus(string msg) : base(msg)
        { }
    }
}
