using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities.Configurations
{
    public class EmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }

        public string PopServer { get; set; }
        public int PopPort { get; set; }
        public string PopUsername { get; set; }
        public string PopPassword { get; set; }

        public EmailConfiguration()
        {
            SmtpServer = "smtp.yandex.ru";
            SmtpPort = 25;
            SmtpUsername = "login @yandex.ru";
            SmtpPassword = "password";
        }
    }
}
