using System;
using System.Collections.Generic;
using System.Text;

namespace SteamKiller.BLL.Entities.Configurations
{
    public class ResourceConfiguration
    {
        public readonly string ROOT_PATH;
        public readonly string AVATAR_PATH;

        public ResourceConfiguration(string rootPath)
        {
            ROOT_PATH = rootPath;
            AVATAR_PATH = @"\images\avatars";
        }
    }
}
