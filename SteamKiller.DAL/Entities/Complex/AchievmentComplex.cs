using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Entities.Complex
{
    public class AchievmentComplex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Reached { get; set; }
        public int ApplicationId { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
