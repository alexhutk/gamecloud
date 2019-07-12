using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamKiller.DPL.Abstract;

namespace SteamKiller.DPL.Abstract
{
    public abstract class Status
    {
        public virtual string State { get; set; }
        public virtual string Message { get; set; }

        public Status(string msg)
        {
            Message = msg;
        }
    }
}
