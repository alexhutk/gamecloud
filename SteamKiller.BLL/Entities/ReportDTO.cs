using SteamKiller.BLL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public int ReporterId { get; set; }
        public string ReporterName { get; set; }
        public string ManagerName { get; set; }
        public ReportType Type { get; set; }
        public string Message { get; set; }
    }
}
