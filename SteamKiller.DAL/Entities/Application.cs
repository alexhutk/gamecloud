using SteamKiller.DAL.Entites.Links;
using SteamKiller.DAL.Entities;
using SteamKiller.DAL.Entities.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Entites
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public Leaderboard Leaderboard { get; set; }

        public List<AppAcc> AppAccs { get; set; }
        public List<AppAch> AppAches { get; set; }
        public List<AppAccSave> AppAccSaves { get; set; }

        public Application()
        {
            AppAccs = new List<AppAcc>();
            AppAches = new List<AppAch>();
            AppAccSaves = new List<AppAccSave>();
        }
    }
}
