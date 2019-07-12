using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.BLL.Services.Implementation
{
    public class ReportService : IReportService
    {
        private ISenderService sender;

        public ReportService(ISenderService _sender)
        {
            sender = _sender;
        }

        public async Task Send(ReportDTO repDTO)
        {
            sender.Send(repDTO);

            /*TODO: Choose a manager and save report to DB*/
        }
    }
}
