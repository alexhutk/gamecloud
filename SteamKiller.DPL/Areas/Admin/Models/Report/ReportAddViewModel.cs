using SteamKiller.BLL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamKiller.WEB.Areas.Admin.Models.Report
{
    public class ReportAddViewModel
    {
        public int AccId { get; set; }
        public ReportType Type { get; set; }
        public string Message { get; set; }
    }
}
