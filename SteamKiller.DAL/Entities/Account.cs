using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Entities;
using SteamKiller.DAL.Entities.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Entites
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }

        public FinanceProfile FinanceProfile { get; set; }

        public List<AccLeader> AccLeaders { get; set; }
        public List<AppAcc> AppAccs { get; set; }
        public List<AccAch> AccAches { get; set; }
        public List<AppAccSave> AppAccSaves { get; set; }

        public Account()
        {
            AccLeaders = new List<AccLeader>();
            AppAccs = new List<AppAcc>();
            AccAches = new List<AccAch>();
            AppAccSaves = new List<AppAccSave>();
        }
    }
}
