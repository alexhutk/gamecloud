using SteamKiller.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamKiller.BLL.Services.Interfaces
{
    public interface IReportService
    {
        Task Send(ReportDTO repDTO);
    }
}
