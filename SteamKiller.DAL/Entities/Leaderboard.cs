using SteamKiller.DAL.Entites.Links;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SteamKiller.DAL.Entites
{
    public class Leaderboard
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public List<AccLeader> AccLeaders { get; set; }

        public Leaderboard()
        {
            AccLeaders = new List<AccLeader>();
        }
    }
}
