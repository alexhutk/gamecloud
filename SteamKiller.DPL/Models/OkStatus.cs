using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamKiller.DPL.Abstract;

namespace SteamKiller.DPL.Models
{
    public class OkStatus : Status
    {
        public override string State
        {
            get => "Success!";
            set => base.State = value;
        }

        public OkStatus(string msg) : base(msg)
        { }
    }
}
