using SteamKiller.BLL.Entities;
using SteamKiller.BLL.Entities.Configurations;
using SteamKiller.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Services.Implementation
{
    public class EmailSenderService : ISenderService
    {
        private readonly EmailConfiguration emailConfig;

        public EmailSenderService(EmailConfiguration _emailConfig)
        {
            emailConfig = _emailConfig;
        }

        public void Send(ReportDTO repDTO)
        {
            
        }
    }
}
