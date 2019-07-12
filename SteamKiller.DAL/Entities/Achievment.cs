using SteamKiller.DAL.Entities.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Entities
{
    public class Achievment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public List<AppAch> AppAches { get; set; }
        public List<AccAch> AccAches { get; set; }

        public Achievment()
        {
            AppAches = new List<AppAch>();
            AccAches = new List<AccAch>();
        }
    }
}
