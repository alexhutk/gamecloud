using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities
{
    public class SaveGameDTO
    {
        public int Id { get; set; }
        public byte[] SaveData { get; set; }
        public DateTime SaveTime { get; set; }
        public int AccountId { get; set; }
        public int ApplicationId { get; set; }
    }
}
