using SteamKiller.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Services.Interfaces
{
    public interface ISenderService
    {
        void Send(ReportDTO repDTO);
    }
}
