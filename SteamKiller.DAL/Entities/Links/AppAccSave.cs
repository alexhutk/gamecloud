using SteamKiller.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.DAL.Entities.Links
{
    public class AppAccSave
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int CloudSaveId { get; set; }
        public CloudSave CloudSave { get; set; }
    }
}
