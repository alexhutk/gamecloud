using SteamKiller.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities
{
    public class ApplicationCollectionDTO
    {
        public List<ApplicationDTO> Applications { get; set; }

        public ApplicationCollectionDTO()
        {
            Applications = new List<ApplicationDTO>();
        }
    }
}
